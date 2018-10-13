using System;

namespace Birdy.Services.PhotoSource.File
{
    public class FilePhotoSourceConfig
    {
        public WorkingDirectory[] workingDirectories { get; set; }
    }
    
    public class WorkingDirectory
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}