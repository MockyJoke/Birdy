using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services.PhotoSource.File.Models;

namespace Birdy.Services.PhotoSource.File
{
    public class FilePhotoSource : IPhotoSource
    {
        private char pathDelimitor;
        public List<string> WorkingDirectories { get; private set; }
        public string Id { get; private set; }
        public FilePhotoSource(List<string> workingDirectories) :
        this(workingDirectories, System.IO.Path.DirectorySeparatorChar)
        {

        }

        public FilePhotoSource(List<string> workingDirectories, char pathDelimitor)
        {
            this.WorkingDirectories = workingDirectories;
            this.pathDelimitor = pathDelimitor;
            this.Id = Guid.NewGuid().ToString();
        }
        public IEnumerable<IAlbumCollection> AlbumCollections
        {
            get
            {
                return WorkingDirectories.Select(wd => new AlbumCollection(wd, Id));
            }
        }

        public Task<Stream> GetImageStreamAsync(IPhoto photo)
        {
            if (!(photo is Photo))
            {
                throw new ArgumentException();
            }
            Photo photoFile = photo as Photo;
            return Task.FromResult<Stream>(GetFileMemoryStream(photoFile.FullFilePath));
        }

        private MemoryStream GetFileMemoryStream(string path)
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                fs.CopyTo(ms);
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}