using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.DataAccess.Model
{
    public class User : BaseItem
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public override string ToString()
        {
            return $"{ID}, {UserName}, {AccessToken}";
        }
    }
}
