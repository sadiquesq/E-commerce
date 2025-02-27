using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs.CategoryDTO;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace E_Commerce.Services.CategoryServices
{

  
    public class CategoryServices : ICategoryServices
    {

        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public CategoryServices(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }


        public async Task<bool> AddCategory(CategoryInsertDto cdo)
        {
            try
            {
                var de = await _mainDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == cdo.CategoryName);
                if (de != null)
                {
                    return false;
                }
                var ne = _mapper.Map<Category>(cdo);
                await _mainDbContext.Categories.AddAsync(ne);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<CaregoryViewDto>> Viewcategorys()
        {

            try
            {
                var p = await _mainDbContext.Categories.ToListAsync();
                return _mapper.Map<List<CaregoryViewDto>>(p);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
