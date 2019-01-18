using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Model
{
    public class CustomResponse
    {
        public bool IsSuccess { get; set; }
        public string AccessToken { get; set; }
        public string SuccessMessage { get; set; }
    }
}
