namespace MMTowers.Service.Model
{
    public class DieselRefill
    {
        public int BTSId { get; set; }
        public string RefillBy { get; set; }
        public int InitialQuantity { get; set; }
        public int FilledQuantity { get; set; }
        public string HMRPhoto { get; set; }
        public string EBPhoto { get; set; }
        public string GSUPhoto { get; set; }
        public string DieselBillPhoto { get; set; }
    }
}
