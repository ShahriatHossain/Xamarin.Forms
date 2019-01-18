using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Model
{
    public class ChannelMaster
    {
        public ChannelApi y_json { get; set; }
        public List<ChannelDetailApi> o_json { get; set; }
    }
}
