using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Model
{
    public class SiteDetail
    {
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string Customer { get; set; }
        public string Circle { get; set; }
        public string Cluster { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtString { get; set; }
    }
}
