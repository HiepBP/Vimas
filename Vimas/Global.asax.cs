using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vimas.Models.Entities;
using Vimas.ViewModels;
using System.Web.Http;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;

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
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Messages";
            DefaultModelBinder.ResourceClassKey = "Messages";

            Vimas.ApiEndpoint.Entry(this.AdditionalMapperConfig);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
          new JsonSerializerSettings
          {
              DateFormatHandling = DateFormatHandling.IsoDateFormat,
              DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
              Culture = CultureInfo.GetCultureInfo("vi-VN")
          };

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("vi-VN");
        }

        public void AdditionalMapperConfig(IMapperConfiguration config)
        {
            config.CreateMap<ThongTinGiaDinh, ThongTinGiaDinhViewModel>();
            config.CreateMap<ThongTinGiaDinhViewModel, ThongTinGiaDinh>();
        }
    }
}
