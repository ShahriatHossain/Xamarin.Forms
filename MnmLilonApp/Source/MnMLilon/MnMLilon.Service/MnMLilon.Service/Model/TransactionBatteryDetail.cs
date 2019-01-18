using System;
using System.Collections.Generic;
using MnMLilon.Service.DataAccess.Model;
using Newtonsoft.Json;

namespace MnMLilon.Service.Model
{
    public class TransactionBatteryDetail
    {
        public int Id { get; set; }
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

        public bool CurrentCommissioned { get; set; }
    }
}
