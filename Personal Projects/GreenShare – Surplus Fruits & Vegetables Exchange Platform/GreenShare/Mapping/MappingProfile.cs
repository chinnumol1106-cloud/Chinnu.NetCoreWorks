using AutoMapper;
using Domain.Models;
using GreenShare.DTOs.RequestDto.Admin;
using GreenShare.DTOs.RequestDto.Auth;
using GreenShare.DTOs.RequestDto.Householder;
using GreenShare.DTOs.RequestDto.Profile;
using GreenShare.DTOs.ResponseDto.Admin;
using GreenShare.DTOs.ResponseDto.Buyer;
using GreenShare.DTOs.ResponseDto.Householder;
using GreenShare.DTOs.ResponseDto.Pay;
using GreenShare.DTOs.ResponseDto.Profile;

namespace GreenShare.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(d => d.PasswordHash,
                    o => o.MapFrom(s => BCrypt.Net.BCrypt.HashPassword(s.Password)));


            CreateMap<AddItemTypeRequestDto, ItemType>();
            CreateMap<AddItemNameRequestDto, ItemName>();

            CreateMap<ItemType, ItemTypeDto>();
            CreateMap<ItemName, ItemNameDto>();

            CreateMap<User, UserListDto>();

            CreateMap<Interest, AdminInterestResponseDto>()
    .ForMember(d => d.InterestId,
        o => o.MapFrom(s => s.Id))

    .ForMember(d => d.ItemId,
        o => o.MapFrom(s => s.ItemId))

    .ForMember(d => d.ItemName,
        o => o.MapFrom(s => s.Item.ItemName.Name))

    .ForMember(d => d.SellerId,
        o => o.MapFrom(s => s.Item.SellerId))

    .ForMember(d => d.SellerName,
        o => o.MapFrom(s => s.Item.Seller.Name))

    .ForMember(d => d.BuyerId,
        o => o.MapFrom(s => s.BuyerId))

    .ForMember(d => d.BuyerName,
        o => o.MapFrom(s => s.Buyer.Name))

    .ForMember(d => d.Status,
        o => o.MapFrom(s => s.Status));



            //seller

            CreateMap<CreateItemRequestDto, Item>()
             .ForMember(d => d.Images, o => o.Ignore())
             .ForMember(dest => dest.PriceUnit, opt => opt.MapFrom(src => src.PricePerUnit)); ;




            CreateMap<Interest, InterestResponseDto>()
    .ForMember(d => d.InterestId,
        o => o.MapFrom(s => s.Id))          // ✅ THIS FIXES IT
    .ForMember(d => d.ItemId,
        o => o.MapFrom(s => s.ItemId))
    .ForMember(d => d.ItemName,
        o => o.MapFrom(s => s.Item.ItemName.Name))
    .ForMember(d => d.BuyerName,
        o => o.MapFrom(s => s.Buyer.Name))
    .ForMember(d => d.InterestStatus,
        o => o.MapFrom(s => s.Status));



            //Buyer
            CreateMap<Interest, BuyerInterestResponseDto>()
    .ForMember(d => d.InterestId,
        o => o.MapFrom(s => s.Id))          // ✅ THIS IS THE FIX
    .ForMember(d => d.ItemId,
        o => o.MapFrom(s => s.ItemId))
    .ForMember(d => d.ItemName,
        o => o.MapFrom(s => s.Item.ItemName.Name))
    .ForMember(d => d.InterestStatus,
        o => o.MapFrom(s => s.Status));





            CreateMap<Item, BuyerItemResponseDto>()
       .ForMember(d => d.ItemId, o => o.MapFrom(s => s.Id))
    .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemName.Name))

    // ✅ FIXED
    .ForMember(d => d.PricePerUnit, o => o.MapFrom(s => s.PriceUnit))

    // ✅ CALCULATED SEPARATELY
    .ForMember(d => d.TotalPrice,
        o => o.MapFrom(s => s.Quantity * s.PriceUnit))

    .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
    .ForMember(d => d.IsOrganic, o => o.MapFrom(s => s.IsOrganic))
    .ForMember(d => d.City, o => o.MapFrom(s => s.Seller.Profile.City))
    .ForMember(d => d.ImagePath,
        o => o.MapFrom(s => s.Images.FirstOrDefault().ImagePath))

    // 🔴 SELLER DETAILS
    .ForMember(d => d.SellerName,
        o => o.MapFrom(s => s.Seller.Name))

    .ForMember(d => d.SellerEmail,
        o => o.MapFrom(s => s.Seller.Email))

    .ForMember(d => d.SellerPhone,
        o => o.MapFrom(s => s.Seller.Phone))

    .ForMember(d => d.ItemStatus, o => o.MapFrom(s => s.Status));


            //profile

            CreateMap<CreateProfileRequestDto, UserProfile>();


            CreateMap<UserProfile, UserProfileResponseDto>()
    .ForMember(d => d.ProfileId,
        o => o.MapFrom(s => s.Id))
    .ForMember(d => d.UserName,
        o => o.MapFrom(s => s.User.Name))
    .ForMember(d => d.Email,
        o => o.MapFrom(s => s.User.Email));




            CreateMap<Item, MyItemResponseDto>()
    .ForMember(d => d.ItemId, o => o.MapFrom(s => s.Id))
    .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemName.Name));

            CreateMap<Interest, MyItemInterestDto>()
    .ForMember(d => d.InterestId,
        o => o.MapFrom(s => s.Id))   
    .ForMember(d => d.BuyerName,
        o => o.MapFrom(s => s.Buyer.Name))
    .ForMember(d => d.InterestStatus,
        o => o.MapFrom(s => s.Status.ToString()));




            //Payment
            CreateMap<Payment, PaymentResponseDto>()
            .ForMember(d => d.PaymentId, o => o.MapFrom(s => s.Id))
    .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
    .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.Status))
    .ForMember(d => d.PaidAt, o => o.MapFrom(s => s.CreatedAt));


        }
    }
}
