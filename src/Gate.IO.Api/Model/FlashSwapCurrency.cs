namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Currencies supported in flash swap
    /// </summary>
    
    public class FlashSwapCurrency :  IEquatable<FlashSwapCurrency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlashSwapCurrency" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public FlashSwapCurrency()
        {
        }

        /// <summary>
        /// Currency name
        /// </summary>
        /// <value>Currency name</value>
        [JsonProperty("currency")]
        public string Currency { get; private set; }

        /// <summary>
        /// Minimum amount required in flash swap
        /// </summary>
        /// <value>Minimum amount required in flash swap</value>
        [JsonProperty("min_amount")]
        public string MinAmount { get; private set; }

        /// <summary>
        /// Maximum amount allowed in flash swap
        /// </summary>
        /// <value>Maximum amount allowed in flash swap</value>
        [JsonProperty("max_amount")]
        public string MaxAmount { get; private set; }

        /// <summary>
        /// Currencies which can be swapped to from this currency
        /// </summary>
        /// <value>Currencies which can be swapped to from this currency</value>
        [JsonProperty("swappable")]
        public List<string> Swappable { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FlashSwapCurrency {\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  MinAmount: ").Append(MinAmount).Append("\n");
            sb.Append("  MaxAmount: ").Append(MaxAmount).Append("\n");
            sb.Append("  Swappable: ").Append(Swappable).Append("\n");
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
            return this.Equals(input as FlashSwapCurrency);
        }

        /// <summary>
        /// Returns true if FlashSwapCurrency instances are equal
        /// </summary>
        /// <param name="input">Instance of FlashSwapCurrency to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FlashSwapCurrency input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.MinAmount == input.MinAmount ||
                    (this.MinAmount != null &&
                    this.MinAmount.Equals(input.MinAmount))
                ) && 
                (
                    this.MaxAmount == input.MaxAmount ||
                    (this.MaxAmount != null &&
                    this.MaxAmount.Equals(input.MaxAmount))
                ) && 
                (
                    this.Swappable == input.Swappable ||
                    this.Swappable != null &&
                    input.Swappable != null &&
                    this.Swappable.SequenceEqual(input.Swappable)
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
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                if (this.MinAmount != null)
                    hashCode = hashCode * 59 + this.MinAmount.GetHashCode();
                if (this.MaxAmount != null)
                    hashCode = hashCode * 59 + this.MaxAmount.GetHashCode();
                if (this.Swappable != null)
                    hashCode = hashCode * 59 + this.Swappable.GetHashCode();
                return hashCode;
            }
        }
    }

}
