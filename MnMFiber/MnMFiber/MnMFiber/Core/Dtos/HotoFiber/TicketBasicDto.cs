namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class TicketBasicDto
    {
        public long TicketId { get; set; }
        public string TicketNo { get; set; }
        public string LinkName { get; set; }
        public string SpanFrom { get; set; }
        public string SpanTo { get; set; }
        public string StatusName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public bool ReAssignToPatroller { get; set; }

        public string SpanFromTo
        {
            get { return SpanFrom + " to " + SpanTo; }
        }

        public string RowBgColor
        {
            get
            {
                if (ReAssignToPatroller)
                {
                    return "#22B14C";
                }
                else
                {
                    return "#6363CB";
                }
            }
        }
    }
}
