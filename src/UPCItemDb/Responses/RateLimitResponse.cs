using System;

namespace UPCItemDb.Responses
{
    public class RateLimitResponse
    {
        public int Remaining { get; set; }
        public int Limit { get; set; }
        public int Current { get; set; }
        public DateTime Reset { get; set; }
    }
}