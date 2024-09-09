using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using short_clips_web_api.Interfaces;
using short_clips_web_api.Models;

namespace short_clips_web_api.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class ShortClipsController : ControllerBase
    {
        private readonly ILogger<ShortClipsController> logger;
        private readonly IShortClipsService shortClipsService;

        private readonly IOptions<AppSettings> config;

        private readonly string projectRootPath;

        /// <summary>
        /// The ShortClips controller. This contains the create, retrieve, update, and delete endpoints.
        /// </summary>
        /// <param name="shortClipsService"></param>
        /// <param name="logger"></param>
        public ShortClipsController(
            IOptions<AppSettings> config,
            IShortClipsService shortClipsService,
            ILogger<ShortClipsController> logger)
        {
            this.config = config;
            this.shortClipsService = shortClipsService;
            this.logger = logger;

            this.projectRootPath = config.Value.ProjectRootPath;
        }

        /// <summary>
        /// Retrieves the list of short clip videos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllShortClips")]
        public async Task<ActionResult<List<Video>>> GetAllShortClipsAsync()
        {
            try
            {
                // get list
                var videos = await this.shortClipsService.GetAllShortClipsAsync();

                // return
                return videos;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the short clip video.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetShortClip")]
        public async Task<ActionResult<Video>> GetShortClipAsync(int id)
        {
            try
            {
                // get video
                var video = await this.shortClipsService.GetShortClipAsync(id);

                return video;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("Stream")]
        public async Task<IActionResult> Stream(int id)
        {
            try
            {
                var video = await this.shortClipsService.GetShortClipAsync(id);
                if (video == null)
                {
                    throw new Exception("Video not found");
                }

                // Stream the video file as a response
                var videoFilePath = Path.Combine(this.projectRootPath, video.VideoFilePath);

                var stream = new FileStream(videoFilePath, FileMode.Open, FileAccess.Read);

                return new FileStreamResult(stream, video.VideoContentType);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("Image")]
        public async Task<IActionResult> Image(int id)
        {
            try
            {
                var video = await this.shortClipsService.GetShortClipAsync(id);
                if (video == null)
                {
                    throw new Exception("Video not found");
                }

                // Stream the video file as a response
                var thumbnailFilePath = Path.Combine(this.projectRootPath, video.ThumbnailFilePath);

                var image = new FileStream(thumbnailFilePath, FileMode.Open, FileAccess.Read);

                return File(image, "image/png");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("UploadShortClip")]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> UploadShortClipAsync(string title, string description, int categoryId)
        {
            try
            {
                var videoFileToUpload = Request.Form.Files[0];

                var newVideoDetails = new VideoRequest()
                {
                    Title = title,
                    Description = description,
                    CategoryId = categoryId,
                };

                var videoUploaded = await this.shortClipsService.CreateShortClipAsync(videoFileToUpload, newVideoDetails);

                return new OkObjectResult(videoUploaded.ToJson());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Updates the short clip video.
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShortClip")]
        public async Task<ActionResult<Video>> UpdateShortClipAsync(Video videoToUpdate)
        {
            try
            {
                // update
                var videoUpdated = await this.shortClipsService.UpdateShortClipAsync(videoToUpdate.Id, videoToUpdate);

                // return
                return videoUpdated;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteShortClip")]
        public async Task<ActionResult<Video>> DeleteShortClipAsync(int id)
        {
            try
            {
                // delete
                var videoDeleted = await this.shortClipsService.DeleteShortClipAsync(id);

                return videoDeleted;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
