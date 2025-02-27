using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Services.ProductServices
{

  
    public class ProductServices : IProductServices
    {

        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;



        public ProductServices(MainDbContext mainDbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }



        public async Task<bool> AddProduct(ProductDTO dTO,IFormFile img)
        {
            try
            {
                var de = await _mainDbContext.Products.FirstOrDefaultAsync(c => c.ProductName == dTO.ProductName);
                if (de != null)
                {
                    return false;
                }
                var n = _mapper.Map<Product>(dTO);
                if (img != null && img.Length > 0)
                {
                    var FileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "Product", FileName);
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }
                    n.image = FileName;
                }
                await _mainDbContext.Products.AddAsync(n);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<ProductViewDto>> Viewproducts()
        {
            try
            {
                var p = await _mainDbContext.Products.ToListAsync();
                if (p != null)
                {
                    var img = p.Select(p =>
                    new ProductViewDto
                    {
                        ProductId = p.ProductId,
                        image = $"{_configuration["HostUrl:Images"]}/Product/{p.image}",
                        Price = p.Price,
                        ProductName = p.ProductName,
                        stock = p.stock
                    });
                    return img.ToList();

                }
                return _mapper.Map<List<ProductViewDto>>(p);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public async Task<List<ProductViewDto>> ViewByCategory(string categoryname)
        {
            try
            {
                var p = await _mainDbContext.Categories.Include(r => r.Products).FirstOrDefaultAsync(e => e.CategoryName == categoryname);
                return _mapper.Map<List<ProductViewDto>>(p.Products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ProductDTO> Viewproductbyid(Guid id)
        {
            try
            {
                var p = await _mainDbContext.Products.FindAsync(id);
                return _mapper.Map<ProductDTO>(p);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductViewDto>> SearchProducts(string name)
        {
            try
            {
                var p = await _mainDbContext.Products.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToListAsync();
                return _mapper.Map<List<ProductViewDto>>(p);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> EditProduct(Guid id, productview1 product,IFormFile image)
        {
            try
            {
                var data = await _mainDbContext.Products.FindAsync(id);
                if (data == null)
                {
                    return false;
                }
                int SomeId;
                //if (!int.TryParse(product.ProductCategoryId.ToString(), out SomeId))
                //{
                //    throw new Exception("invalid category id");
                //}
                string SomeImage = null;
                if (image != null && image.Length > 0)
                {
                    var FileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var DirectoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "Product");
                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }
                    var FilePath = Path.Combine(DirectoryPath, FileName);
                    using (var Stream = new FileStream(FilePath, FileMode.Create))
                    {
                        await image.CopyToAsync(Stream);
                        SomeImage = FileName;
                    }
                }


                data.ProductName = product.ProductName;
                data.Price = product.Price;
                data.image = SomeImage;
                data.stock = product.stock;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                var n = await _mainDbContext.Products.FindAsync(id);
                if (n == null)
                {
                    return false;
                }
                _mainDbContext.Products.Remove(n);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<PagedResult<ProductViewDto>> viewProductByPagination(paginationParams paginationParams)
        {
            try
            {
                var total = await _mainDbContext.Products.CountAsync();

                var products = await _mainDbContext.Products
                        .OrderBy(p => p.ProductId)
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync();


                var productDtos = _mapper.Map<List<ProductViewDto>>(products);

                return new PagedResult<ProductViewDto>
                {
                    TotalRecords = total,
                    CurrentPage = paginationParams.PageNumber,
                    PageSize = paginationParams.PageSize,
                    Data = productDtos
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





    }
}
