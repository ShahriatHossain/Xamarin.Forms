using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Model
{
    public class Message
    {
        public DateTime SendTime { get; set; }
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public int Id { get; set; }
        public bool EnableVoting { get; set; }
        public DateTime? VotingExpireDate { get; set; }
        public bool IsVoteCasted { get; set; }
        public string FileType { get; set; }
        public string FileDisplayName { get; set; }
        public bool IsVoteOver { get; set; }
        public string VotingStatus { get; set; }
    }
}
