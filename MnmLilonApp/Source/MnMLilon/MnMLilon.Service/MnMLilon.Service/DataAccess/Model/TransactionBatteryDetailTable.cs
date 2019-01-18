namespace MnMLilon.Service.DataAccess.Model
{
    public class TransactionBatteryDetailTable : BaseItem
    {
        public int ServerBatteryId { get; set; }
        public int TransactionId { get; set; }
        public int SerialNo { get; set; }
        public string BatteryName { get; set; }
        public decimal? PackVoltage { get; set; }
        public decimal? SocPercent { get; set; }
        public decimal? SohPercent { get; set; }
        public decimal? CellVoltageMin { get; set; }
        public decimal? CellVoltageMax { get; set; }
        public decimal? CellTempMin { get; set; }
        public decimal? CellTempMax { get; set; }
        public decimal? MosfetTemp { get; set; }
    }
}
