using System.ComponentModel.DataAnnotations;

namespace short_clips_web_api.Models
{
    public class VideoRequest
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
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
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the video thumbnail.
        /// </summary>
        public string? Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the video content.
        /// </summary>
        public byte[]? VideoContent { get; set; }

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
}
