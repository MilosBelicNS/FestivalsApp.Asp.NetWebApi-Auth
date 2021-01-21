using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using FestivalApp.Asp.Net.WebApi.Interfaces;
using FestivalApp.Asp.Net.WebApi.Repository;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using ProductService.Resolver;

namespace FestivalApp.Asp.Net.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

                );

            //unity container
            var container = new UnityContainer();
            container.RegisterType<IPlaceRepository, PlaceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFestivalRepository, FestivalRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

        }
    }
}
