using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Extras.Web.Enrichers;
using ShouldITweetClient.App_Start;
using ShouldITweetClient.Data;
using ShouldITweetClient.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShouldITweetClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            BuildMappings(builder);

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //AreaRegistration.RegisterAllAreas();

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureLogging();
        }

        private static void BuildMappings(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseVerbotenPhraseProvider>().As<IVerbotenPhraseProvider>();
            builder.RegisterType<VerbotenChecker>().As<IVerbotenChecker>();
            builder.RegisterType<VerbotenPhraseRepository<VerbotenPhrase>>().As<IRepository<VerbotenPhrase>>();
        }

        private void ConfigureLogging()
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Source", "Web")
                .Enrich.WithProperty("ServiceAccount", Environment.UserName)
                .Enrich.WithProperty("ApplicationVersion", GetType().Assembly.GetName().Version)
                .Enrich.With<HttpRequestClientHostIPEnricher>()
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<HttpRequestNumberEnricher>()
                .Enrich.With<HttpRequestRawUrlEnricher>()
                .Enrich.With<HttpRequestTraceIdEnricher>()
                .Enrich.With<HttpRequestTypeEnricher>()
                .Enrich.With<HttpRequestUrlReferrerEnricher>()
                .Enrich.With<HttpRequestUserAgentEnricher>()
                .Enrich.With<UserNameEnricher>()
                .WriteTo.RollingFile(ConfigurationManager.AppSettings["LogFilePath"])
                .WriteTo.Seq(ConfigurationManager.AppSettings["SeqUrl"])
                .CreateLogger();

            Log.Logger = logger;
        }
    }
}
