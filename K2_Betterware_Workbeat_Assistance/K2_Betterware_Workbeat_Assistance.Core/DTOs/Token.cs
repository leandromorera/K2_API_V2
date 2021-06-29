using System;
using System.Collections.Generic;
using System.Text;

namespace K2_Betterware_Workbeat_Assistance.Core.DTOs
{
    public class Token
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
    }
}
