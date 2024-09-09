using short_clips_web_api.Models;

namespace short_clips_web_api.Interfaces
{
    public interface ICategoriesService
    {
        /// <summary>
        /// Gets the list of categories.
        /// </summary>
        /// <returns>Returns the list of video categories.</returns>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        /// Gets the category by id.
        /// </summary>
        /// <param name="id">The category id.</param>
        /// <returns>Returns the category.</returns>
        Task<Category> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="category">The category to create.</param>
        /// <returns>Returns the category created.</returns>
        Task<Category> CreateCategorydAsync(Category category);
    }
}
