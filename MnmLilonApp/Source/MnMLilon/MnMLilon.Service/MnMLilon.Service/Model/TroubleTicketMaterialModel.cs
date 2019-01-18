using System;

namespace MnMLilon.Service.Model
{
    public class TroubleTicketMaterialModel
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public int SparePartId { get; set; }

        public string PartDescription { get; set; }
        public string PartSerialNo { get; set; }

        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsRepaired { get; set; }
    }
}
