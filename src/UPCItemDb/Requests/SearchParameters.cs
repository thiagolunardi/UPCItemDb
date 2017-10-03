using System;
using Newtonsoft.Json;

namespace UPCItemDb.Requests
{
    public class SearchParameters
    {
        public SearchParameters(string keywords)
        {
            if(string.IsNullOrEmpty(keywords) || string.IsNullOrWhiteSpace(keywords))
                throw new ArgumentException("Invalid keywords search term", "keywords");

            Keywords = keywords;
        }

        /// <summary>
        /// Search keyworkds
        /// </summary>
        [JsonProperty(PropertyName = "s")]
        public string Keywords { get; set; }
        /// <summary>
        /// Search brand name
        /// </summary>
        [JsonProperty(PropertyName = "brand", NullValueHandling = NullValueHandling.Ignore)]
        public string Brand { get; set; }
        /// <summary>
        /// Product category keyword
        /// </summary>
        [JsonProperty(PropertyName = "category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        /// <summary>
        /// Offset for result paging. '0' indicates no more
        /// </summary>
        [JsonProperty(PropertyName = "offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "match_mode")]
        public MatchMode MatchMode { get; set; } = MatchMode.Default;
        /// <summary>
        /// "product" (default) or "book"
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } = "product";
    }
}