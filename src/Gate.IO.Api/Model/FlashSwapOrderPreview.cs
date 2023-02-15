namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Initiate a flash swap order preview
    /// </summary>
    
    public class FlashSwapOrderPreview :  IEquatable<FlashSwapOrderPreview>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlashSwapOrderPreview" /> class.
        /// </summary>
        /// <param name="previewId">Preview result ID.</param>
        /// <param name="sellCurrency">Currency to sell which can be retrieved from supported currency list API &#x60;GET /flash_swap/currencies&#x60;.</param>
        /// <param name="sellAmount">Amount to sell.</param>
        /// <param name="buyCurrency">Currency to buy which can be retrieved from supported currency list API &#x60;GET /flash_swap/currencies&#x60;.</param>
        /// <param name="buyAmount">Amount to buy.</param>
        /// <param name="price">Price.</param>
        public FlashSwapOrderPreview(string previewId = default(string), string sellCurrency = default(string), string sellAmount = default(string), string buyCurrency = default(string), string buyAmount = default(string), string price = default(string))
        {
            this.PreviewId = previewId;
            this.SellCurrency = sellCurrency;
            this.SellAmount = sellAmount;
            this.BuyCurrency = buyCurrency;
            this.BuyAmount = buyAmount;
            this.Price = price;
        }

        /// <summary>
        /// Preview result ID
        /// </summary>
        /// <value>Preview result ID</value>
        [JsonProperty("preview_id")]
        public string PreviewId { get; set; }

        /// <summary>
        /// Currency to sell which can be retrieved from supported currency list API &#x60;GET /flash_swap/currencies&#x60;
        /// </summary>
        /// <value>Currency to sell which can be retrieved from supported currency list API &#x60;GET /flash_swap/currencies&#x60;</value>
        [JsonProperty("sell_currency")]
        public string SellCurrency { get; set; }

        /// <summary>
        /// Amount to sell
        /// </summary>
        /// <value>Amount to sell</value>
        [JsonProperty("sell_amount")]
        public string SellAmount { get; set; }

        /// <summary>
        /// Currency to buy which can be retrieved from supported currency list API &#x60;GET /flash_swap/currencies&#x60;
        /// </summary>
        /// <value>Currency to buy which can be retrieved from supported currency list API &#x60;GET /flash_swap/currencies&#x60;</value>
        [JsonProperty("buy_currency")]
        public string BuyCurrency { get; set; }

        /// <summary>
        /// Amount to buy
        /// </summary>
        /// <value>Amount to buy</value>
        [JsonProperty("buy_amount")]
        public string BuyAmount { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        /// <value>Price</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FlashSwapOrderPreview {\n");
            sb.Append("  PreviewId: ").Append(PreviewId).Append("\n");
            sb.Append("  SellCurrency: ").Append(SellCurrency).Append("\n");
            sb.Append("  SellAmount: ").Append(SellAmount).Append("\n");
            sb.Append("  BuyCurrency: ").Append(BuyCurrency).Append("\n");
            sb.Append("  BuyAmount: ").Append(BuyAmount).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as FlashSwapOrderPreview);
        }

        /// <summary>
        /// Returns true if FlashSwapOrderPreview instances are equal
        /// </summary>
        /// <param name="input">Instance of FlashSwapOrderPreview to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FlashSwapOrderPreview input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.PreviewId == input.PreviewId ||
                    (this.PreviewId != null &&
                    this.PreviewId.Equals(input.PreviewId))
                ) && 
                (
                    this.SellCurrency == input.SellCurrency ||
                    (this.SellCurrency != null &&
                    this.SellCurrency.Equals(input.SellCurrency))
                ) && 
                (
                    this.SellAmount == input.SellAmount ||
                    (this.SellAmount != null &&
                    this.SellAmount.Equals(input.SellAmount))
                ) && 
                (
                    this.BuyCurrency == input.BuyCurrency ||
                    (this.BuyCurrency != null &&
                    this.BuyCurrency.Equals(input.BuyCurrency))
                ) && 
                (
                    this.BuyAmount == input.BuyAmount ||
                    (this.BuyAmount != null &&
                    this.BuyAmount.Equals(input.BuyAmount))
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.PreviewId != null)
                    hashCode = hashCode * 59 + this.PreviewId.GetHashCode();
                if (this.SellCurrency != null)
                    hashCode = hashCode * 59 + this.SellCurrency.GetHashCode();
                if (this.SellAmount != null)
                    hashCode = hashCode * 59 + this.SellAmount.GetHashCode();
                if (this.BuyCurrency != null)
                    hashCode = hashCode * 59 + this.BuyCurrency.GetHashCode();
                if (this.BuyAmount != null)
                    hashCode = hashCode * 59 + this.BuyAmount.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                return hashCode;
            }
        }
    }

}
