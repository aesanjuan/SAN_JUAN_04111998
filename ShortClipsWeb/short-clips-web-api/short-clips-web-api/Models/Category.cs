using System.ComponentModel.DataAnnotations;

namespace short_clips_web_api.Models
{
    public class Category
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string? Name { get; set; }
    }
}
