using System.Collections.Generic;
using System.IO;
using System.Linq;
using Birdy.Controllers;
using Birdy.Models;
using Microsoft.Extensions.Logging;

namespace Birdy.Services.PhotoSource.File.Models
{
    public class Album : IAlbum
    {
        private AlbumCollection albumCollection;

        public Album(AlbumCollection albumCollection, string path)
        {
            this.albumCollection = albumCollection;
            this.FilePath = path;
        }

        public string FilePath { get; private set; }

        public string FullFilePath
        {
            get
            {
                return $"{albumCollection.FilePath}{Path.DirectorySeparatorChar}{FilePath}";
            }
        }

        public string Id
        {
            get
            {
                return Name;
            }
        }

        public string Name
        {
            get
            {
                return FilePath;
            }
        }

        public IAlbumCollection Parent
        {
            get
            {
                return albumCollection;
            }
        }

        public IEnumerable<IPhoto> Photos
        {
            get
            {
                ILogger<PhotosController> logger = PhotosController.SharedLogger;
                logger.LogWarning("Enumerating files for: "+FullFilePath);
                return Directory.EnumerateFiles(FullFilePath, "*.JPG").Select(f => new Photo(this, Path.GetFileName(f)));
            }
        }
    }
}