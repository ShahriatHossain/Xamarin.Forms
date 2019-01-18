using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Model
{
    public class ChannelDetail: Channel
    {
        public IEnumerable<Message> Messages { get; set; }
    }
}
