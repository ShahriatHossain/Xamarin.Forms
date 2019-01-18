namespace MnMFiber.Core.Models.HotoFiber
{
    public class TicketSignatureUploadModel
    {
        public long TicketId { get; set; }
        public string PatrollerSignatureFileName { get; set; }
        public string CustomerRepresentativeFileName { get; set; }
        public string ThirdPartyRepresentativeFileName { get; set; }

        public string PatrollerSignatureFileDisplayName { get; set; }
        public string CustomerRepresentativeFileDisplayName { get; set; }
        public string ThirdPartyRepresentativeFileDisplayName { get; set; }
        public string CustomerRepresentativeName { get; set; }
        public string CustomerRepresentativeMobileNo { get; set; }

        public string ThirdPartyRepresentativeName { get; set; }
        public string ThirdPartyRepresentativeMobileNo { get; set; }
    }
}
