using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Model
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public bool Flag { get; set; }
        public bool Flag2 { get; set; }

        public string BgColor { get; set; }
    }
}
