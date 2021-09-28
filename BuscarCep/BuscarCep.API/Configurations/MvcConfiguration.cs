using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BuscarCep.Services.Api.Configurations
{
    public static class MvcConfiguration
    {
        public static void UseCentralRoutePrefix(this MvcOptions mvcOptions, IRouteTemplateProvider routeAttribute)
        {
            mvcOptions.Conventions.Insert(0, new RouteConvention(routeAttribute));
        }
    }
}
