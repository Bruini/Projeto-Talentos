using System.Web.Http;
using WebActivatorEx;
using Fatec.RD.WebApi;
using Swashbuckle.Application;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Fatec.RD.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Fatec.RD.Api");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi(c => { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\bin\Fatec.RD.WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
