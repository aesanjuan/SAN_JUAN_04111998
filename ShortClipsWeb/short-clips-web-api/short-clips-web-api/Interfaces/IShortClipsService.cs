using short_clips_web_api.Models;

namespace short_clips_web_api.Interfaces
{
    /// <summary>
    /// The Short Clips Service Interface.
    /// </summary>
    public interface IShortClipsService
    {
        /// <summary>
        /// Gets all short clip videos.
        /// </summary>
        /// <returns>Returns a list of short clip videos.</returns>
        Task<List<Video>> GetAllShortClipsAsync();

        /// <summary>
        /// Gets a short clip video.
        /// </summary>
        /// <param name="id">The video id.</param>
        /// <returns>Returns the short clip video.</returns>
        Task<Video> GetShortClipAsync(int id);

        /// <summary>
        /// Creates a short clip video.
        /// </summary>
        /// <param name="newVideoToUpload">The video to upload.</param>
        /// <param name="newVideoDetails">The video details to save in db.</param>
        /// <returns>Returns the created video.</returns>
        Task<Video> CreateShortClipAsync(IFormFile newVideoToUpload, VideoRequest newVideoDetails);

        /// <summary>
        /// Updates a short clip video.
        /// </summary>
        /// <param name="id">The video id to update.</param>
        /// <param name="videoToUpdate">The video object to update.</param>
        /// <returns>Returns the updated video.</returns>
        Task<Video> UpdateShortClipAsync(int id, Video videoToUpdate);

        /// <summary>
        /// Deletes a short clip video by id.
        /// </summary>
        /// <param name="id">The video id to delete.</param>
        Task<Video> DeleteShortClipAsync(int id);
    }
}
