using AutoMapper;
using GalvProducts.Api.Business.Contracts;
using GalvProducts.Api.Common;
using GalvProducts.Api.Data.Contracts;
using System;

namespace GalvProducts.Api
{
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Automapper configuration
        /// </summary>
        public AutoMapping()
        {
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<ProductModel, ProductEntity>();
            CreateMap<ProductEntity, ProductModel>();
            CreateMap<ProductCreateViewModel, ProductModel>();
            CreateMap<ProductInputViewModel, ProductInputModel>().ForMember(des=> des.Currency, src=> src.MapFrom((s, d) => {
                Enum.TryParse(s.Currency, out CurrencyEnum currency);
                return currency;
            }));
        }
    }
}
