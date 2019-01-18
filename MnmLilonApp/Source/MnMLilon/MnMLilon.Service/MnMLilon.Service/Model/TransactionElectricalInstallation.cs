using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MnMLilon.Service.DataAccess.Model;
using Newtonsoft.Json;

namespace MnMLilon.Service.Model
{
    public class TransactionElectricalInstallation
    {
        public bool BusBarToBatterPowerConnectionOk { get; set; }
        public bool ModuleConnectionOk { get; set; }
        public bool PowerPlantConnectionOk { get; set; }
        public bool BatteryComConnectionOk { get; set; }
        public bool PowerConnectionOk { get; set; }
        public string BusBarToBatterPowerConnectionComment { get; set; }
        public string ModuleConnectionComment { get; set; }
        public string PowerPlantConnectionComment { get; set; }
        public string BatteryComConnectionComment { get; set; }
        public string PowerConnectionComment { get; set; }
    }
}
