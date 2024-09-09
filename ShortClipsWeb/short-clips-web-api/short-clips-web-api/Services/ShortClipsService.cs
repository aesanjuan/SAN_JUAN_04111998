using Microsoft.Extensions.Options;
using short_clips_web_api.Interfaces;
using short_clips_web_api.Models;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace short_clips_web_api.Services
{
    /// <summary>
    /// The Short Clips service.
    /// </summary>
    public class ShortClipsService : IShortClipsService
    {
        private AppDbContext _context;

        private readonly ICategoriesService _categoriesService;

        private readonly IOptions<AppSettings> config;

        private readonly string projectRootPath;
        private readonly string uploadsFolderPartialPath = @"ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\Videos"; // move this to constants
        private readonly string thumbnailsFolderPartialPath = @"ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\Thumbnails"; // move this to constants

        public ShortClipsService(
            IOptions<AppSettings> config,
            AppDbContext context,
            ICategoriesService categoriesService)
        {
            this.config = config;
            this._context = context;
            this._categoriesService = categoriesService;

            this.projectRootPath = config.Value.ProjectRootPath;
        }

        /// <inheritdoc/>
        public async Task<List<Video>> GetAllShortClipsAsync()
        {
            try
            {
                // get all videos
                var videoList = this._context.Set<Video>().Where(x => x.IsDeleted == false).ToList();
                return videoList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Video> GetShortClipAsync(int id)
        {
            try
            {
                // get video by id
                var video =  this._context.Find<Video>(id);

                // check if existing, return error if not existing
                if (video == null)
                {
                    throw new Exception("Video does not exist");
                }

                // return result
                return video;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Video> CreateShortClipAsync(IFormFile newVideoToUpload, VideoRequest newVideoDetails)
        {
            try
            {
                // check if video is null
                if (newVideoToUpload == null)
                {
                    throw new Exception("The video is empty.");
                }

                // validate extensions
                var validExtensions = new List<string>() { ".mp4" , ".avi", ".mov" };
                var newVideoExtension = Path.GetExtension(newVideoToUpload.FileName);
                if (!validExtensions.Contains(newVideoExtension))
                {
                    throw new Exception("File extension is invalid.");
                }

                // check size if within limit, return error if not
                var newVideoFileSizeInMB = newVideoToUpload.Length / (1024 * 1024);
                var maxFileSizeInMB = 100; // set size limit to 100mb
                if (newVideoFileSizeInMB > maxFileSizeInMB)
                {
                    throw new Exception("Video size has exceeded the maximum. Video must be less than or equal to 100mb.");
                }                

                // get category based on id
                var videoCategory = await this._categoriesService.GetCategoryByIdAsync(newVideoDetails.CategoryId);
                if (videoCategory == null)
                {
                    throw new Exception($"The video category with id '{newVideoDetails.CategoryId}' does not exist.");
                }

                // create video details object to be inserted in database
                var newVideoToInsert = new Video()
                {
                    Title = newVideoDetails.Title,
                    Description = newVideoDetails.Description,
                    Category = videoCategory,                    
                    IsDeleted = false,
                    UploadDateTime = DateTime.UtcNow,
                    LastUpdatedDateTime = DateTime.UtcNow,
                };

                // check if all details are valid before proceeding to save everything
                if (newVideoToInsert.IsValid())
                {
                    // video & thumbnail will have same file names
                    var newVideoGuidAsFileName = Guid.NewGuid().ToString();

                    // create video upload file path (sets path where video will be saved)
                    var uploadsFolderFullPath = Path.Combine(this.projectRootPath, this.uploadsFolderPartialPath);
                    if (!Directory.Exists(uploadsFolderFullPath))
                    {
                        Directory.CreateDirectory(uploadsFolderFullPath);
                    }

                    var videoFilePath = Path.Combine(this.uploadsFolderPartialPath, newVideoGuidAsFileName + newVideoExtension);

                    // save video to folder by copying the video file to the '/Uploads/Videos' folder
                    var videoFilePathWithRoot = Path.Combine(this.projectRootPath, videoFilePath);
                    using (var stream = new FileStream(videoFilePathWithRoot, FileMode.Create))
                    {
                        newVideoToUpload.CopyTo(stream);
                    }

                    // create a thumbnail using Xabe.FFmepg, returns path where thumbnail is created
                    var thumbnailPath = await this.GenerateThumbnail(videoFilePathWithRoot, newVideoGuidAsFileName);

                    newVideoToInsert.ThumbnailFilePath = thumbnailPath;
                    newVideoToInsert.VideoFilePath = videoFilePath;
                    newVideoToInsert.VideoContentType = newVideoToUpload.ContentType;

                    // save to database
                    await this._context.AddAsync<Video>(newVideoToInsert);
                    await this._context.SaveChangesAsync();

                    // verify
                    newVideoToInsert = this._context.Set<Video>().First(x => x.VideoFilePath.ToLower() == videoFilePath.ToLower());
                }

                return newVideoToInsert;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Video> UpdateShortClipAsync(int id, Video videoToUpdate)
        {
            try
            {
                // check first if the id or videoToUpdate are not null before proceeding
                if (id == 0 || videoToUpdate == null)
                {
                    throw new Exception("The video id or video content is missing.");
                }

                // get video by id & check if exists
                var videoExisting = await this.GetShortClipAsync(id);
                if (videoExisting == null)
                {
                    throw new Exception("The video cannot be updated because it is not existing.");
                }

                // check if valid
                if (videoToUpdate.IsValid())
                {
                    // update video and save
                    videoExisting.Title = videoToUpdate.Title;
                    videoExisting.Description = videoToUpdate.Description;
                    videoExisting.Category = videoToUpdate.Category;
                    videoExisting.LastUpdatedDateTime = DateTime.UtcNow;

                    this._context.Update<Video>(videoExisting);
                    await this._context.SaveChangesAsync();
                }

                return videoToUpdate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Video> DeleteShortClipAsync(int id)
        {
            try
            {
                // get video by id
                var videoToDelete = await this.GetShortClipAsync(id);

                // check if existing, return error if not
                if (videoToDelete == null)
                {
                    throw new Exception("Video does not exist to delete.");
                }

                // delete video
                _context.Remove<Video>(videoToDelete);
                await this._context.SaveChangesAsync();

                return videoToDelete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates the thumbnail based on video being uploaded.
        /// </summary>
        /// <param name="videoFilePath">The full video file path where video is uploaded.</param>
        /// <returns></returns>
        private async Task<string> GenerateThumbnail(string videoFilePath, string thumbnailFileName)
        {
            try
            {
                // download ffmpeg when needed (using Xabe.FFmpeg.Downloader)
                var ffmpegPath = Path.Combine(this.projectRootPath, @"ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\FFmpeg\ffmpeg");
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, ffmpegPath);

                // locate ffmpeg.exe
                FFmpeg.SetExecutablesPath(ffmpegPath);

                // set output path (where thumbnail will be stored)
                string outputPathPartial = Path.Combine(this.thumbnailsFolderPartialPath, thumbnailFileName + ".png");
                string output = Path.Combine(this.projectRootPath, outputPathPartial);

                // generate thumbnail
                IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(videoFilePath, output, TimeSpan.FromSeconds(0));
                IConversionResult result = await conversion.Start();

                return outputPathPartial;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}