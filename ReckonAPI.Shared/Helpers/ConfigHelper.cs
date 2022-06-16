using System;
using System.Collections.Generic;
using System.Text;

namespace ReckonAPI.Shared.Helpers
{
    public static class ConfigHelper
    {
        public static int RetryCount { get; set; }
        public static int RetryWaitMs { get; set; }
        public static string SubTextUrl { get; set; }
        public static string TextToSearchUrl { get; set; }
        public static string PostResultUrl { get; set; }
    }

    
}
