using short_clips_web_api.Interfaces;
using short_clips_web_api.Models;

namespace short_clips_web_api.Services
{
    public class CategoriesService : ICategoriesService
    {
        private AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            this._context = context;
        }

        /// <inheritdoc/>
        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var categoryList = this._context.Set<Category>().ToList();

                return categoryList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = this._context.Find<Category>(id);

                if (category == null)
                {
                    throw new Exception($"Category with id '{id}' is not existing");
                }

                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Category> CreateCategorydAsync(Category category)
        {
            try
            {
                var categoryExists = this._context.Set<Category>().FirstOrDefault(x => x.Name.ToLower().Equals(category.Name, StringComparison.CurrentCultureIgnoreCase));

                if (category == null)
                {
                    throw new Exception($"Category already exists.");
                }

                await this._context.AddAsync<Category>(category);
                await this._context.SaveChangesAsync();

                // verify
                var categoryCreated = this._context.Set<Category>().First(x => x.Name.ToLower() == category.Name.ToLower());

                return categoryCreated;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
