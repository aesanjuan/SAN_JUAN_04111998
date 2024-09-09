using System.ComponentModel.DataAnnotations;

namespace short_clips_web_api.Models
{
    /// <summary>
    /// The Video class.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the video content-type for streaming.
        /// </summary>
        public string? VideoContentType { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail file path where the thumbnail is stored upon uploading the video.
        /// </summary>
        public string? ThumbnailFilePath { get; set; }
        
        /// <summary>
        /// Gets or sets the video file path where the video is stored upon uploading.
        /// </summary>
        public string? VideoFilePath { get; set; }

        /// <summary>
        /// Gets or sets the video whether it is soft-deleted or not.
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the upload date & time. Acts as created datetime.
        /// </summary>
        public DateTime? UploadDateTime { get; set; }

        /// <summary>
        /// Gets or sets the last updated date time.
        /// </summary>
        public DateTime? LastUpdatedDateTime { get; set; }
    }

    /// <summary>
    /// The Video class extension.
    /// </summary>
    static class VideoExtensions
    {
        public static bool IsValid(this Video video)
        {
            // check if title is null or contains more than 80 characters
            if (video.Title == null || video.Title?.Length > 80)
            {
                throw new Exception("The video title is missing or over 80 characters.");
            }

            // check if description is null or contains more than 80 characters
            if (video.Description == null || video.Description?.Length > 160)
            {
                throw new Exception("The video description is missing or over 160 characters.");
            }

            // check if video content is null
            //if (string.IsNullOrEmpty(video.VideoFilePath))
            //{
            //    throw new Exception("The video content is missing.");
            //}

            // check if video thumbnail is null
            //if (string.IsNullOrEmpty(video.ThumbnailFilePath))
            //{
            //    throw new Exception("The video thumbnail is missing.");
            //}            

            return true;
        }
    }
}
