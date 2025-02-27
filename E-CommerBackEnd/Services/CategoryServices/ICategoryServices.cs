using E_Commerce.DTOs.CategoryDTO;

namespace E_Commerce.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<bool> AddCategory(CategoryInsertDto cdo);

        Task<List<CaregoryViewDto>> Viewcategorys();


    }
}
