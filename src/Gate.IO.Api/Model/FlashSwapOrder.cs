namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Flash swap order
    /// </summary>
    
    public class FlashSwapOrder :  IEquatable<FlashSwapOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlashSwapOrder" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public FlashSwapOrder()
        {
        }

        /// <summary>
        /// Flash swap order ID
        /// </summary>
        /// <value>Flash swap order ID</value>
        [JsonProperty("id")]
        public long Id { get; private set; }

        /// <summary>
        /// Creation time of order (in milliseconds)
        /// </summary>
        /// <value>Creation time of order (in milliseconds)</value>
        [JsonProperty("create_time")]
        public long CreateTime { get; private set; }

        /// <summary>
        /// User ID
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("user_id")]
        public long UserId { get; private set; }

        /// <summary>
        /// Currency to sell
        /// </summary>
        /// <value>Currency to sell</value>
        [JsonProperty("sell_currency")]
        public string SellCurrency { get; private set; }

        /// <summary>
        /// Amount to sell
        /// </summary>
        /// <value>Amount to sell</value>
        [JsonProperty("sell_amount")]
        public string SellAmount { get; private set; }

        /// <summary>
        /// Currency to buy
        /// </summary>
        /// <value>Currency to buy</value>
        [JsonProperty("buy_currency")]
        public string BuyCurrency { get; private set; }

        /// <summary>
        /// Amount to buy
        /// </summary>
        /// <value>Amount to buy</value>
        [JsonProperty("buy_amount")]
        public string BuyAmount { get; private set; }

        /// <summary>
        /// Price
        /// </summary>
        /// <value>Price</value>
        [JsonProperty("price")]
        public string Price { get; private set; }

        /// <summary>
        /// Flash swap order status  &#x60;1&#x60; - success &#x60;2&#x60; - failure
        /// </summary>
        /// <value>Flash swap order status  &#x60;1&#x60; - success &#x60;2&#x60; - failure</value>
        [JsonProperty("status")]
        public int Status { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FlashSwapOrder {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  SellCurrency: ").Append(SellCurrency).Append("\n");
            sb.Append("  SellAmount: ").Append(SellAmount).Append("\n");
            sb.Append("  BuyCurrency: ").Append(BuyCurrency).Append("\n");
            sb.Append("  BuyAmount: ").Append(BuyAmount).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
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
            return this.Equals(input as FlashSwapOrder);
        }

        /// <summary>
        /// Returns true if FlashSwapOrder instances are equal
        /// </summary>
        /// <param name="input">Instance of FlashSwapOrder to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FlashSwapOrder input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.CreateTime == input.CreateTime ||
                    this.CreateTime.Equals(input.CreateTime)
                ) && 
                (
                    this.UserId == input.UserId ||
                    this.UserId.Equals(input.UserId)
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
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
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
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.CreateTime.GetHashCode();
                hashCode = hashCode * 59 + this.UserId.GetHashCode();
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
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                return hashCode;
            }
        }
    }

}
