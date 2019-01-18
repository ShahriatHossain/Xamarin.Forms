using System;

namespace MnMLilon.Service.Model
{
    public class SparePartModel
    {
        public int Id { get; set; }
        public string PartDescription { get; set; }
        public string PartSerialNo { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
