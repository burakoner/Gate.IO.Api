namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsSettlement
    /// </summary>
    
    public class OptionsSettlement :  IEquatable<OptionsSettlement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsSettlement" /> class.
        /// </summary>
        /// <param name="time">Last changed time of configuration.</param>
        /// <param name="contract">Options contract name.</param>
        /// <param name="profit">Settlement profit per size (quote currency).</param>
        /// <param name="fee">Settlement fee per size (quote currency).</param>
        /// <param name="strikePrice">Strike price (quote currency).</param>
        /// <param name="settlePrice">Settlement price (quote currency).</param>
        public OptionsSettlement(double time = default(double), string contract = default(string), string profit = default(string), string fee = default(string), string strikePrice = default(string), string settlePrice = default(string))
        {
            this.Time = time;
            this.Contract = contract;
            this.Profit = profit;
            this.Fee = fee;
            this.StrikePrice = strikePrice;
            this.SettlePrice = settlePrice;
        }

        /// <summary>
        /// Last changed time of configuration
        /// </summary>
        /// <value>Last changed time of configuration</value>
        [JsonProperty("time")]
        public double Time { get; set; }

        /// <summary>
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("contract")]
        public string Contract { get; set; }

        /// <summary>
        /// Settlement profit per size (quote currency)
        /// </summary>
        /// <value>Settlement profit per size (quote currency)</value>
        [JsonProperty("profit")]
        public string Profit { get; set; }

        /// <summary>
        /// Settlement fee per size (quote currency)
        /// </summary>
        /// <value>Settlement fee per size (quote currency)</value>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Strike price (quote currency)
        /// </summary>
        /// <value>Strike price (quote currency)</value>
        [JsonProperty("strike_price")]
        public string StrikePrice { get; set; }

        /// <summary>
        /// Settlement price (quote currency)
        /// </summary>
        /// <value>Settlement price (quote currency)</value>
        [JsonProperty("settle_price")]
        public string SettlePrice { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsSettlement {\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  Profit: ").Append(Profit).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  StrikePrice: ").Append(StrikePrice).Append("\n");
            sb.Append("  SettlePrice: ").Append(SettlePrice).Append("\n");
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
            return this.Equals(input as OptionsSettlement);
        }

        /// <summary>
        /// Returns true if OptionsSettlement instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsSettlement to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsSettlement input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Time == input.Time ||
                    this.Time.Equals(input.Time)
                ) && 
                (
                    this.Contract == input.Contract ||
                    (this.Contract != null &&
                    this.Contract.Equals(input.Contract))
                ) && 
                (
                    this.Profit == input.Profit ||
                    (this.Profit != null &&
                    this.Profit.Equals(input.Profit))
                ) && 
                (
                    this.Fee == input.Fee ||
                    (this.Fee != null &&
                    this.Fee.Equals(input.Fee))
                ) && 
                (
                    this.StrikePrice == input.StrikePrice ||
                    (this.StrikePrice != null &&
                    this.StrikePrice.Equals(input.StrikePrice))
                ) && 
                (
                    this.SettlePrice == input.SettlePrice ||
                    (this.SettlePrice != null &&
                    this.SettlePrice.Equals(input.SettlePrice))
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
                hashCode = hashCode * 59 + this.Time.GetHashCode();
                if (this.Contract != null)
                    hashCode = hashCode * 59 + this.Contract.GetHashCode();
                if (this.Profit != null)
                    hashCode = hashCode * 59 + this.Profit.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                if (this.StrikePrice != null)
                    hashCode = hashCode * 59 + this.StrikePrice.GetHashCode();
                if (this.SettlePrice != null)
                    hashCode = hashCode * 59 + this.SettlePrice.GetHashCode();
                return hashCode;
            }
        }
    }

}
