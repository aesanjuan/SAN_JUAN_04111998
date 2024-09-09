using Microsoft.AspNetCore.Mvc;
using short_clips_web_api.Interfaces;
using short_clips_web_api.Models;

namespace short_clips_web_api.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ICategoriesService categoriesService,
            ILogger<CategoriesController> logger)
        {
            this.categoriesService = categoriesService;
            this._logger = logger;
        }

        /// <summary>
        /// Retrieves all available categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var categoryList = await this.categoriesService.GetCategoriesAsync();

                if (categoryList == null)
                {
                    throw new Exception("There are no video categories available.");
                }

                return categoryList;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<ActionResult<Category>> CreateCategoryAsync(Category category)
        {
            try
            {
                var categoryCreated = await this.categoriesService.CreateCategorydAsync(category);

                return categoryCreated;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
