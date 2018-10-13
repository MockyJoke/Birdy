using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services;
using Birdy.Services.Caching;
using Birdy.Services.Caching.Simple;
using Birdy.Services.ImageManipulation;
using Birdy.Services.PhotoSource;
using Birdy.Services.PhotoSource.File;
using Birdy.Util;
using Birdy.Util.Extraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Birdy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            FilePhotoSourceConfig filePhotoSourceConfig = new FilePhotoSourceConfig();

            Configuration.GetSection("FilePhotoSourceConfig").Bind(filePhotoSourceConfig);

            IPhotoSource photoSource = new FilePhotoSource(filePhotoSourceConfig);

            ICachingService<string, byte[]> cachingService = new SimpleInMemCachingService<string, byte[]>();
            IImageManipulationService imageManipulationService = new PortableImageManipulationService();
            services.AddSingleton<IPhotoService>(new CachedPhotoService(
                new List<IPhotoSource> { photoSource },
                cachingService,
                imageManipulationService
                ));

            AlbumCollectionExtractor albumCollectionExtractor = new AlbumCollectionExtractor();
            AlbumExtractor albumExtractor = new AlbumExtractor(albumCollectionExtractor);
            PhotoExtractor photoExtractor = new PhotoExtractor(albumExtractor);
            HashGenerator hashGenerator = new HashGenerator();
            services.AddSingleton(albumCollectionExtractor);
            services.AddSingleton(albumExtractor);
            services.AddSingleton(photoExtractor);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Setup Angular SPA https://dzone.com/articles/create-an-application-with-angular-6-and-net-core
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer("start");
                }
            });
        }
    }
}
