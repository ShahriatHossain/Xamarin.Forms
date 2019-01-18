using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Interface
{
    public interface ILocalFileManageService
    {
        byte[] GetFileDataArray(string filePath);
    }
}
