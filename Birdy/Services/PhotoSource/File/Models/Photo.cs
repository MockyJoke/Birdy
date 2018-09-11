
using System;
using System.IO;
using Birdy.Models;

namespace Birdy.Services.PhotoSource.File.Models
{
    public class Photo : IPhoto
    {
        private Album album;

        public Photo(Album album, string path)
        {
            this.album = album;
            this.FilePath = path;
        }

        public string FilePath { get; private set; }

        public string FullFilePath
        {
            get
            {
                return $"{album.FullFilePath}{Path.DirectorySeparatorChar}{FilePath}";
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

        public IAlbum Parent
        {
            get
            {
                return album;
            }
        }

        public int CompareTo(IPhoto other)
        {
            string thisId = $"{Id}.{Parent.Id}.{Parent.Id}";
            string otherId = $"{other.Id}.{other.Parent.Id}.{other.Parent.Id}";
            return thisId.CompareTo(otherId);
        }

        public override int GetHashCode(){
            string thisId = $"{Id}.{Parent.Id}.{Parent.Id}";
            return thisId.GetHashCode();
        }

        public override bool Equals(object obj){
            return GetHashCode().Equals(obj.GetHashCode());
        }
    }
}