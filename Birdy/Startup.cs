using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services;
using Birdy.Services.Caching;
using Birdy.Services.Caching.MongoDB;
using Birdy.Services.Caching.Native;
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
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

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
            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            IPhotoSource photoSource = ProvidePhotoSource();
            IHashGenerator hashGenerator = ProvideHashGenerator();
            ICachingService<string, byte[]> cachingService = ProvideCachingService(hashGenerator);
            IImageManipulationService imageManipulationService = ProvideImageManipulationService();
            IPhotoService photoService = ProvidePhotoService(photoSource, cachingService, imageManipulationService);
            services.AddSingleton<IPhotoService>(photoService);
            services.AddSingleton<IHashGenerator>(hashGenerator);

            AlbumCollectionExtractor albumCollectionExtractor = new AlbumCollectionExtractor();
            AlbumExtractor albumExtractor = new AlbumExtractor(albumCollectionExtractor);
            PhotoExtractor photoExtractor = new PhotoExtractor(albumExtractor);
            services.AddSingleton(albumCollectionExtractor);
            services.AddSingleton(albumExtractor);
            services.AddSingleton(photoExtractor);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Setup Angular SPA https://dzone.com/articles/create-an-application-with-angular-6-and-net-core
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

        #region Providers

        private IPhotoSource ProvidePhotoSource()
        {
            FilePhotoSourceConfig filePhotoSourceConfig = new FilePhotoSourceConfig();
            Configuration.GetSection("FilePhotoSourceConfig").Bind(filePhotoSourceConfig);
            IPhotoSource photoSource = new FilePhotoSource(filePhotoSourceConfig);
            return photoSource;
        }

        private ICachingService<string, byte[]> ProvideCachingService(IHashGenerator hashGenerator)
        {
            // ICachingService<string, byte[]> cachingService = new NativeMemoryCachingService<string, byte[]>();
            string connectionString = Configuration["MongoDbConnectionString"];
            ICachingService<string, byte[]> cachingService = new MongoDbCachingService<string, byte[]>(new MongoClient(connectionString), "Birdy", "ThumbnailData", hashGenerator);
            return cachingService;
        }

        private IImageManipulationService ProvideImageManipulationService()
        {
            IImageManipulationService imageManipulationService = new SkiaImageManipulationService();
            return imageManipulationService;
        }

        private IHashGenerator ProvideHashGenerator()
        {
            IHashGenerator hashGenerator = new HashGenerator();
            return hashGenerator;
        }

        private IPhotoService ProvidePhotoService(
            IPhotoSource photoSource,
            ICachingService<string, byte[]> cachingService,
            IImageManipulationService imageManipulationService
            )
        {
            IPhotoService photoService = new CachedPhotoService(
                new List<IPhotoSource> { photoSource },
                cachingService,
                imageManipulationService
                );
            return photoService;
        }

        #endregion
    }
}
