namespace MnMLilon.Service.Model
{
    public class CustomTroubleTicketMaterialModel
    {
        public long Id { get; set; }
        public int SparePartId { get; set; }
        public string PartDescription { get; set; }
        public string PartSerialNo { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public string MaterialType { get; set; }

    }
}
