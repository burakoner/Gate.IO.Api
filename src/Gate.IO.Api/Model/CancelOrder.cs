namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Info of order to be cancelled
    /// </summary>
    public class CancelOrder :  IEquatable<CancelOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelOrder" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CancelOrder() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelOrder" /> class.
        /// </summary>
        /// <param name="currencyPair">Order currency pair (required).</param>
        /// <param name="id">Order ID or user custom ID. Custom ID are accepted only within 30 minutes after order creation (required).</param>
        /// <param name="account">If cancelled order is cross margin order or is portfolio margin account&#39;s API key, this field must be set and can only be &#x60;cross_margin&#x60;If cancelled order is cross margin order, this field must be set and can only be &#x60;cross_margin&#x60;.</param>
        public CancelOrder(string currencyPair = default(string), string id = default(string), string account = default(string))
        {
            // to ensure "currencyPair" is required (not null)
            this.CurrencyPair = currencyPair ?? throw new ArgumentNullException("currencyPair", "currencyPair is a required property for CancelOrder and cannot be null");
            // to ensure "id" is required (not null)
            this.Id = id ?? throw new ArgumentNullException("id", "id is a required property for CancelOrder and cannot be null");
            this.Account = account;
        }

        /// <summary>
        /// Order currency pair
        /// </summary>
        /// <value>Order currency pair</value>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }

        /// <summary>
        /// Order ID or user custom ID. Custom ID are accepted only within 30 minutes after order creation
        /// </summary>
        /// <value>Order ID or user custom ID. Custom ID are accepted only within 30 minutes after order creation</value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// If cancelled order is cross margin order or is portfolio margin account&#39;s API key, this field must be set and can only be &#x60;cross_margin&#x60;If cancelled order is cross margin order, this field must be set and can only be &#x60;cross_margin&#x60;
        /// </summary>
        /// <value>If cancelled order is cross margin order or is portfolio margin account&#39;s API key, this field must be set and can only be &#x60;cross_margin&#x60;If cancelled order is cross margin order, this field must be set and can only be &#x60;cross_margin&#x60;</value>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CancelOrder {\n");
            sb.Append("  CurrencyPair: ").Append(CurrencyPair).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Account: ").Append(Account).Append("\n");
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
            return this.Equals(input as CancelOrder);
        }

        /// <summary>
        /// Returns true if CancelOrder instances are equal
        /// </summary>
        /// <param name="input">Instance of CancelOrder to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CancelOrder input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CurrencyPair == input.CurrencyPair ||
                    (this.CurrencyPair != null &&
                    this.CurrencyPair.Equals(input.CurrencyPair))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Account == input.Account ||
                    (this.Account != null &&
                    this.Account.Equals(input.Account))
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
                if (this.CurrencyPair != null)
                    hashCode = hashCode * 59 + this.CurrencyPair.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Account != null)
                    hashCode = hashCode * 59 + this.Account.GetHashCode();
                return hashCode;
            }
        }
    }

}
