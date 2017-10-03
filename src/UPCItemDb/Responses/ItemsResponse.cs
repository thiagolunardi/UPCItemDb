using Newtonsoft.Json;

namespace UPCItemDb.Responses
{
    public class ItemsResponse
    {
        public string Code { get; set; }
        public int Total { get; set; }
        public int OffSet { get; set; }
        public Item[] Items { get; set; }
        public RateLimitResponse RateLimit { get; set; }

        public ItemsResponse()
        {
            RateLimit = new RateLimitResponse();
        }
    }

    public class Item
    {
        /// <summary>
        /// EAN-13, 13-digit European Article Number (aka. GTIN-13). This is the unique number we used to identify each item in our database. If it starts with 0, the rest 12-digit is the UPC (aka. UPC-A, GTIN-12), ex. 0885909456017.
        /// </summary>
        public string EAN { get; set; }
        /// <summary>
        /// Item title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// UPC-A, 12-digit Universal Product Code (aka. GTIN-12). If item’s EAN does not start with 0, there is no corresponding UPC-A code, ex. 6009705662678.
        /// </summary>
        public string UPC { get; set; }
        /// <summary>
        /// GTIN-14, 14-digit number used to identify trade items at various packaging levels. The contained trade item’s EAN or UPC can be derived from it. Ex. GTIN-14 20008236914225 contains 20-Pack of item with UPC 008236914221.
        /// </summary>
        public string GTIN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ASIN { get; set; }
        /// <summary>
        /// Item description with length < 515.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Brand name or manufacture name with length < 64.
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Item model number with length < 32.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Item color with length < 32, ex. for clothing, shoes.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Item size with length < 32, ex. for clothing, shoes.
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Item model number with length < 32.
        /// </summary>
        public string Dimension { get; set; }
        /// <summary>
        /// Item weight with length < 16.
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// currency of the lowest_recorded_price. Can be “USD”, “CAD”, “EUR”, “GBP”, “SEK”. Default “” means “USD”.
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Lowest historical price of the item since tracked by our system. Not available for books.
        /// </summary>
        [JsonProperty(PropertyName = "lowest_recorded_price")]
        public decimal LowestRecordedPrice { get; set; }
        /// <summary>
        /// Highest historical price of the item since tracked by our system. Not available for books.
        /// </summary>
        [JsonProperty(PropertyName = "highest_recorded_price")]
        public decimal HighestRecordedPrice { get; set; }
        /// <summary>
        /// Array of image urls.
        /// </summary>
        public string[] Images { get; set; }
        /// <summary>
        /// Array of Offers
        /// </summary>
        public Offer[] Offers { get; set; }
        /// <summary>
        ///  For user to correlate the response with original request. The same value with max length of 32 will be returned with the response if user set it in the request.
        /// </summary>
        [JsonProperty(PropertyName = "user_data")]
        public string UserData { get; set; }
    }

    public class Offer
    {
        /// <summary>
        /// Online store name.
        /// </summary>
        public string Merchant { get; set; }
        /// <summary>
        /// Online store domain.
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Item name marketed by the merchant.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Currency of the list_price & price. Can be “USD”, “CAD”, “EUR”, “GBP”, “SEK”. Default “” means “USD”.
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Original price from the store.
        /// </summary>
        [JsonProperty(PropertyName = "list_price")]
        public string ListPrice { get; set; }
        /// <summary>
        /// Sale price.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// “Free Shipping” or other shipping information if not free.
        /// </summary>
        public string Shipping { get; set; }
        /// <summary>
        /// “New” or “Used”
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// Default “” means available or “Out of Stock”
        /// </summary>
        public string Availability { get; set; }
        /// <summary>
        /// Shop link of the item.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Unix timestamp of the offer was last updated.
        /// </summary>
        [JsonProperty(PropertyName = "updated_t")]
        public long Updated { get; set; }
    }
}