using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Birdy.Services.PhotoSource.File
{
    public class FileAccessControl
    {
        private object queueLock;
        private List<FileAccessRequest> accessList;

        public FileAccessControl()
        {
            queueLock = new object();
            accessList = new List<FileAccessRequest>();
        }

        public Task<T> QueueNewRequest<T>(string fileName, Func<T> fileAction)
        {
            FileAccessRequest request = new FileAccessRequest() { FileName = fileName };
            lock (queueLock)
            {
                accessList.Add(request);
            }
            return Task.Run(() =>
            {
                T result = default(T);
                while (accessList[0].FileName != fileName)
                {
                    request.handle.WaitOne();
                }
                try
                {
                    result = fileAction();
                }
                catch
                {
                    result = default(T);
                }
                lock (queueLock)
                {
                    accessList.Remove(request);
                    if (accessList.Count != 0)
                    {
                        accessList.Sort((item1, item2) =>
                        {
                            return item1.FileName.CompareTo(item2.FileName);
                        });
                        accessList[0].handle.Set();
                    }
                }
                return result;
            });
        }
    }

    class FileAccessRequest
    {
        public AutoResetEvent handle;
        public string FileName { get; set; }
        public FileAccessRequest()
        {
            handle = new AutoResetEvent(false);
        }
    }
}