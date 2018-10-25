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
        public WorkingDirectory[] WorkingDirectories { get; private set; }
        public string Id { get; private set; }
        private FileAccessControl fileAccessControl;

        public FilePhotoSource(FilePhotoSourceConfig filePhotoSourceConfig) :
        this(filePhotoSourceConfig.workingDirectories, System.IO.Path.DirectorySeparatorChar)
        {

        }


        public FilePhotoSource(WorkingDirectory[] workingDirectories, char pathDelimitor)
        {
            this.WorkingDirectories = workingDirectories;
            this.pathDelimitor = pathDelimitor;
            this.Id = Guid.NewGuid().ToString();
            this.fileAccessControl = new FileAccessControl();
        }
        public IEnumerable<IAlbumCollection> AlbumCollections
        {
            get
            {
                return WorkingDirectories.Select(wd => new AlbumCollection(wd.Name, wd.Path, Id));
            }
        }

        public Task<Stream> GetImageStreamAsync(IPhoto photo)
        {
            if (!(photo is Photo))
            {
                throw new ArgumentException();
            }
            Photo photoFile = photo as Photo;
            MemoryStream fileStream = fileAccessControl.QueueNewRequest(photoFile.FullFilePath,() => GetFileMemoryStream(photoFile.FullFilePath)).Result;
            return Task.FromResult<Stream>(fileStream);
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