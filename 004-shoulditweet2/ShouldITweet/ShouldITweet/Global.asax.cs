using Autofac;
using Autofac.Integration.Mvc;
using Serilog;
using Serilog.Extras.Web.Enrichers;
using ShouldITweet2.Data;
using ShouldITweet2.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShouldITweet2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var builder = new ContainerBuilder();

            BuildMappings(builder);

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

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
