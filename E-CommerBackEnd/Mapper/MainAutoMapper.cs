
using AutoMapper;
using E_Commerce.DTOs.AdressDTO;
using E_Commerce.DTOs.cartDTO;
using E_Commerce.DTOs.cartItemDTO;
using E_Commerce.DTOs.CategoryDTO;
using E_Commerce.DTOs.OderDTO;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.DTOs.UserDTO;
using E_Commerce.DTOs.WhishListDto;
using E_Commerce.Models;

namespace E_Commerce.Mapper
{

    public class MainAutoMapper : Profile
    {

        public MainAutoMapper()
        {
            CreateMap<UserInsertDto, User>();

            CreateMap<LoginDto, User>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();


            CreateMap<CategoryInsertDto, Category>().ReverseMap();

            CreateMap<CaregoryViewDto, Category>().ReverseMap();

            CreateMap<ProductDTO, Product>().ReverseMap();

            CreateMap<ProductViewDto, Product>().ReverseMap();

            CreateMap<CartItemDto, CartItem>().ReverseMap();

            CreateMap<CartItemviewDto, CartItem>().ReverseMap()
                .ForMember(e => e.Amount, e => e.MapFrom(s => s.product.Price * s.Quantity))
                .ForMember(e => e.ProductName, e => e.MapFrom(s => s.product.ProductName));


            CreateMap<CartViewDto, Cart>().ReverseMap();

            CreateMap<WhishListDto, WhishList>().ReverseMap();

            CreateMap<OrderViewDTO, Order>().ReverseMap()
                .ForMember(e => e.ProductName, e => e.MapFrom(s => s.Product.ProductName));


            CreateMap<OrderAdminViewdto, Order>().ReverseMap()
                                .ForMember(e => e.ProductName, e => e.MapFrom(s => s.Product.ProductName));

            CreateMap<AddressDto,Address>().ReverseMap();
            CreateMap<AddressViewDio,Address>().ReverseMap();
        }

    }
}
