using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Vimas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Vimas.ApiEndpoint.Entry(this.AdditionalMapperConfig);
        }

        public void AdditionalMapperConfig(IMapperConfiguration config)
        {
            //config.CreateMap<ProductViewModel, ProductViewModel>();
            //config.CreateMap<ProductDetailsViewModel, ProductDetailsViewModel>();
            //config.CreateMap<ProductViewModel, ProductDetailsViewModel>();
            //config.CreateMap<ProductEditViewModel, ProductViewModel>();
            //config.CreateMap<ProductImageCollectionDetailsViewModel, ProductImageCollectionDetailsViewModel>();
            //config.CreateMap<ImageCollectionViewModel, ImageCollectionViewModel>();
            //config.CreateMap<OrderViewModel, OrderViewModel>();

            //config.CreateMap<BlogPostViewModel, BlogPostViewModel>();

            //config.CreateMap<StoreUser, StoreUserViewModel>();
            //config.CreateMap<StoreUserViewModel, StoreUser>();

            //config.CreateMap<BlogPostEditViewModel, BlogPostViewModel>();

            //config.CreateMap<AspNetUserDetailsViewModel, AspNetUserEditViewModel>();
            //config.CreateMap<ProductCategory, HmsService.ViewModels.ProductCategoryEditViewModel>()
            //    .ForMember(q => q.Products, opt => opt.MapFrom(q => q.Products));
        }
    }
}
