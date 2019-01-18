using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Model
{
    public class TroubleCategorySetupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtString { get; set; }
        public int? UpdatedById { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedAtString { get; set; }
    }
}
