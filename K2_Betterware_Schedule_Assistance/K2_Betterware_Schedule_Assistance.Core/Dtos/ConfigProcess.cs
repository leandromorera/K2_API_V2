using System;

namespace K2_Betterware_Schedule_Assistance.Core.Dtos
{
    public class ConfigProcess
    {   
        public string trigger { get; set; }
        public Boolean active { get; set; }
        public Boolean reprocess { get; set; }
        public Boolean insertAssistance { get; set; }

    }
}
