using System.Collections.Generic;

namespace OriginAPI.Models
{
    public class InfoList
    {
        public string friendUserId { get; set; }
    }

    public class Personas
    {
        public ulong totalCount { get; set; }
        public List<InfoList> infoList { get; set; }
    }
}
