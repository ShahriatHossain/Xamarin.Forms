using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Service
{
    public interface IFileStorageService
    {
        Task Upload(Stream stream, string blobName);
    }
}
