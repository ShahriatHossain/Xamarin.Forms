using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Model
{
    public class TechnicianTransaction
    {
        public long Id { get; set; }
        public string TransactionNo { get; set; }
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public bool ReAssignedToTechnician { get; set; }
        public string RowBackgroundColor { get; set; }
    }
}
