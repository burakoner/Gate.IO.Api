namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Liquidate Order detail
    /// </summary>
    
    public class LiquidateOrder :  IEquatable<LiquidateOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidateOrder" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected LiquidateOrder() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidateOrder" /> class.
        /// </summary>
        /// <param name="text">User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) .</param>
        /// <param name="currencyPair">Currency pair (required).</param>
        /// <param name="amount">Trade amount (required).</param>
        /// <param name="price">Order price (required).</param>
        public LiquidateOrder(string text = default(string), string currencyPair = default(string), string amount = default(string), string price = default(string))
        {
            // to ensure "currencyPair" is required (not null)
            this.CurrencyPair = currencyPair ?? throw new ArgumentNullException("currencyPair", "currencyPair is a required property for LiquidateOrder and cannot be null");
            // to ensure "amount" is required (not null)
            this.Amount = amount ?? throw new ArgumentNullException("amount", "amount is a required property for LiquidateOrder and cannot be null");
            // to ensure "price" is required (not null)
            this.Price = price ?? throw new ArgumentNullException("price", "price is a required property for LiquidateOrder and cannot be null");
            this.Text = text;
        }

        /// <summary>
        /// User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) 
        /// </summary>
        /// <value>User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) </value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Currency pair
        /// </summary>
        /// <value>Currency pair</value>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }

        /// <summary>
        /// Trade amount
        /// </summary>
        /// <value>Trade amount</value>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Order price
        /// </summary>
        /// <value>Order price</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LiquidateOrder {\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  CurrencyPair: ").Append(CurrencyPair).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
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
            return this.Equals(input as LiquidateOrder);
        }

        /// <summary>
        /// Returns true if LiquidateOrder instances are equal
        /// </summary>
        /// <param name="input">Instance of LiquidateOrder to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LiquidateOrder input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.CurrencyPair == input.CurrencyPair ||
                    (this.CurrencyPair != null &&
                    this.CurrencyPair.Equals(input.CurrencyPair))
                ) && 
                (
                    this.Amount == input.Amount ||
                    (this.Amount != null &&
                    this.Amount.Equals(input.Amount))
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
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.CurrencyPair != null)
                    hashCode = hashCode * 59 + this.CurrencyPair.GetHashCode();
                if (this.Amount != null)
                    hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                return hashCode;
            }
        }
    }

}
