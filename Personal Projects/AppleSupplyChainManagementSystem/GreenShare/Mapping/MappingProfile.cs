using AutoMapper;
using Domain.Models;
using GreenShare.DTOs.RequestDto.Admin;
using GreenShare.DTOs.RequestDto.Auth;
using GreenShare.DTOs.RequestDto.Householder;
using GreenShare.DTOs.RequestDto.Profile;
using GreenShare.DTOs.ResponseDto.Admin;
using GreenShare.DTOs.ResponseDto.AppleOwner;
using GreenShare.DTOs.ResponseDto.Company;

using GreenShare.DTOs.ResponseDto.Profile;
using GreenShare.DTOs.ResponseDto.Student;

namespace GreenShare.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<AddAppleTypeRequestDto, AppleType>();
            //CreateMap<AddAppleVarietyRequestDto, AppleVariety>();
            CreateMap<AddAppleVarietyRequestDto, AppleVariety>()
    .ForMember(dest => dest.AppleTypeId,
               opt => opt.MapFrom(src => src.TypeId));
            CreateMap<AddAppleGradeRequestDto, AppleGrade>()
                .ForMember(dest=>dest.Grade,opt=>opt.MapFrom(src=>src.Grade));
            CreateMap<AddApplePriceRequestDto, ApplePrice>();

            CreateMap<User, UserResponseDto>();
            CreateMap<AdminMetric, MetricsResponseDto>();


            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmationToken, opt => opt.Ignore());




            CreateMap<CreateCollectionRequestDto, CollectionRequest>();

            CreateMap<CollectionRequest, CollectionRequestResponseDto>()
                .ForMember(dest => dest.AppleVariety,
                    opt => opt.MapFrom(src => src.AppleVariety.Name))
                .ForMember(dest => dest.CollectionStatus,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.PreferredPickupAt,
                    opt => opt.MapFrom(src => src.PreferredPickupAt))
                .ForMember(dest=>dest.WeekNumber,
                opt=>opt.MapFrom(src=>src.WeekNumber));


            CreateMap<StudentAssignment, AdminStudentAssignmentResponseDto>()
    .ForMember(d => d.AssignmentId, o => o.MapFrom(s => s.Id))
    .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student.Name))
    .ForMember(d => d.AppleOwnerName, o => o.MapFrom(s => s.CollectionRequest.User.Name))
    .ForMember(d => d.CollectionRequestId, o => o.MapFrom(s => s.CollectionRequestId))
    .ForMember(d => d.AssignmentStatus, o => o.MapFrom(s => s.Status))
    .ForMember(d => d.AssignedAt, o => o.MapFrom(s => s.AssignedAt))
    .ForMember(d => d.CollectedAt, o => o.MapFrom(s => s.CollectedAt));



            CreateMap<CollectionRequest, AdminCollectionRequestResponseDto>()
    .ForMember(d => d.RequestId, o => o.MapFrom(s => s.Id))
    .ForMember(d => d.AppleOwnerName, o => o.MapFrom(s => s.User.Name))
    .ForMember(d => d.Address, o => o.MapFrom(s => s.User.Profile.Address))
    .ForMember(d => d.AppleVariety, o => o.MapFrom(s => s.AppleVariety.Name))
    .ForMember(d => d.QuantityKg, o => o.MapFrom(s => s.QuantityKg))
    .ForMember(d => d.PreferredPickupAt, o => o.MapFrom(s => s.PreferredPickupAt))
    .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
    .ForMember(d => d.ImageUrl,
        o => o.MapFrom(s => s.ImageUrl))
    .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt));




            CreateMap<StudentAssignment, StudentAssignmentResponseDto>()
                 .ForMember(d => d.AssignmentId, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.AppleOwnerName, o => o.MapFrom(s => s.CollectionRequest.User.Name))
                 .ForMember(d => d.PhoneNumber,
        o => o.MapFrom(s => s.CollectionRequest.User.Profile.PhoneNumber))
                 .ForMember(d => d.Address, o => o.MapFrom(s => s.CollectionRequest.User.Profile.Address))
                  .ForMember(d => d.AppleVariety,
        o => o.MapFrom(s => s.CollectionRequest.AppleVariety.Name))
                 .ForMember(d => d.RequestedKg, o => o.MapFrom(s => s.CollectionRequest.QuantityKg))
                 .ForMember(d => d.CollectionStatus, o => o.MapFrom(s => s.CollectionRequest.Status))
                 .ForMember(d => d.AssignmentStatus, o => o.MapFrom(s => s.Status))
                  .ForMember(d => d.AssignedAt,
        o => o.MapFrom(s => s.AssignedAt));

            CreateMap<AddCompanyPriceRequestDto, CompanyApplePrice>();

            CreateMap<CompanyApplePrice, CompanyPriceResponseDto>()
     .ForMember(d => d.GradeName,
         o => o.MapFrom(s => s.AppleGrade.Grade))
     .ForMember(d => d.VarietyName,
         o => o.MapFrom(s => s.AppleVariety != null ? s.AppleVariety.Name : null));

            CreateMap<CompanyStock, CompanyStockResponseDto>()
           .ForMember(d => d.GradeName,
               o => o.MapFrom(s => s.AppleGrade.Grade));

            CreateMap<ApplePrice, OwnerPriceResponseDto>()
    .ForMember(d => d.AppleType,
        o => o.MapFrom(s => s.AppleType.Name))
    .ForMember(d => d.AppleVariety,
        o => o.MapFrom(s => s.AppleVariety.Name))
    .ForMember(d => d.AppleGrade,
        o => o.MapFrom(s => s.AppleGrade.Grade));


            CreateMap<CompanyAppleRequest, CompanyRequestResponseDto>()
     .ForMember(d => d.RequestId, o => o.MapFrom(s => s.Id))
     .ForMember(d => d.Grade, o => o.MapFrom(s => s.AppleGrade.Grade))
     .ForMember(d => d.Variety, o => o.MapFrom(s => s.AppleVariety != null ? s.AppleVariety.Name : "Mixed"))
     .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

//            CreateMap<CompanyAppleRequest, CompanyRequestResponseDto>()
//.ForMember(d => d.GradeName, o => o.MapFrom(s => s.AppleGrade.Grade))
//.ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<CompanyAppleRequest, AdminCompanyRequestResponseDto>()
.ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company.User.Name))
.ForMember(d => d.GradeName, o => o.MapFrom(s => s.AppleGrade.Grade))
.ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<CreateProfileRequestDto, UserProfile>();
            CreateMap<UpdateProfileRequestDto, UserProfile>();
            CreateMap<UserProfile, ProfileResponseDto>();




            CreateMap<AppleGrade, AvailableGradeResponseDto>()
    .ForMember(d => d.AppleGradeId, o => o.MapFrom(s => s.Id))
    .ForMember(d => d.GradeName, o => o.MapFrom(s => s.Grade));

            CreateMap<ApplePrice, CompanyPriceResponseDto>()
                .ForMember(d => d.GradeName, o => o.MapFrom(s => s.AppleGrade.Grade));

            CreateMap<CompanyStock, CompanyStockResponseDto>()
                .ForMember(d => d.GradeName, o => o.MapFrom(s => s.AppleGrade.Grade));

            //CreateMap<CompanyAppleRequest, CompanyRequestResponseDto>()
            //    .ForMember(d => d.GradeName, o => o.MapFrom(s => s.AppleGrade.Grade));



        }
    }
}
