using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Model
{
    public class Tower
    {
        public int Id { get; set; }
        public string BTSCode { get; set; }
        public string BTSName { get; set; }
        public string BTSAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string CustomerName { get; set; }
        public string CircleName { get; set; }
        public string ClusterName { get; set; }
        public decimal? DGCapacity { get; set; }
        public decimal? DieselTankCapacity { get; set; }
        public decimal? CphMinValue { get; set; }
        public decimal? CphMaxValue { get; set; }
        public int TotalRecord { get; set; }
        public string SuperVisorName { get; set; }
        public string SupervisorContactNumber { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianContactNumber { get; set; }
        public string TowerType { get; set; }
        public string SiteType { get; set; }
        public string SiteScope { get; set; }
        public string SiteSeverity { get; set; }
        public string JcName { get; set; }
    }
}
