using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    //https://localhost:xxxx/api/categories. This is the path which needs to be followed to get to this controller.
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;


        }
        //
        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request) {
            //Map DTO to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };


            await categoryRepository.CreateAsync(category);
            //Domain Model to DTO
            var response = new CategoryDto
            {
                Id=category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //Map Domain Model to DTO
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }


        // Get Edit ID
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute(Name = "Id")] Guid Id)
        {
          var existingcategory=  await categoryRepository.GetById(Id);

            if(existingcategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existingcategory.Id,
                Name = existingcategory.Name,
                UrlHandle = existingcategory.UrlHandle

            };

            return Ok(response);
        }

        //[HttpPut]
        //    [Route("{Id:guid}")]
        //public async Task<IActionResult> EditCategory([FromRoute(Name = "Id")] Guid Id,UpdateCategoryRequestDTO request)
        //{
        //    var category = new Category
        //    {
        //        Id = Id,
        //        Name = request.Name,
        //        UrlHandle = request.UrlHandle
        //    }
        //}




    }
}
