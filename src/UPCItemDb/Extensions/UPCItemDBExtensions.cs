using UPCItemDb.Responses;

namespace UPCItemDb.Extensions
{
    public static class UPCItemDBExtensions
    {
        public static string ToBaseUrl(this UPCItemDBEnvironment environment)
        {
            switch (environment)
            {
                case UPCItemDBEnvironment.Trial: return "trial";
                case UPCItemDBEnvironment.Production: return "v1";
            }
            return "";
        }

        public static ItemsResponse AssignRateLimit(this ItemsResponse itemsResponse, RateLimitResponse rateLimit)
        {
            itemsResponse.RateLimit = rateLimit;
            return itemsResponse;
        }
    }
}
