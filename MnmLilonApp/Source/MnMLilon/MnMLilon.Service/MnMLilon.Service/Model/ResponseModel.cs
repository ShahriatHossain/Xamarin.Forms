using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Model
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? ReturnId { get; set; }
        public string ReturnCode { get; set; }
        public string ErrorCode { get; set; }
    }
}
