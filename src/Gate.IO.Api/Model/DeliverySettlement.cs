namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// DeliverySettlement
    /// </summary>
    
    public class DeliverySettlement :  IEquatable<DeliverySettlement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliverySettlement" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public DeliverySettlement()
        {
        }

        /// <summary>
        /// Liquidation time
        /// </summary>
        /// <value>Liquidation time</value>
        [JsonProperty("time")]
        public long Time { get; private set; }

        /// <summary>
        /// Futures contract
        /// </summary>
        /// <value>Futures contract</value>
        [JsonProperty("contract")]
        public string Contract { get; private set; }

        /// <summary>
        /// Position leverage
        /// </summary>
        /// <value>Position leverage</value>
        [JsonProperty("leverage")]
        public string Leverage { get; private set; }

        /// <summary>
        /// Position size
        /// </summary>
        /// <value>Position size</value>
        [JsonProperty("size")]
        public long Size { get; private set; }

        /// <summary>
        /// Position margin
        /// </summary>
        /// <value>Position margin</value>
        [JsonProperty("margin")]
        public string Margin { get; private set; }

        /// <summary>
        /// Average entry price
        /// </summary>
        /// <value>Average entry price</value>
        [JsonProperty("entry_price")]
        public string EntryPrice { get; private set; }

        /// <summary>
        /// Settled price
        /// </summary>
        /// <value>Settled price</value>
        [JsonProperty("settle_price")]
        public string SettlePrice { get; private set; }

        /// <summary>
        /// Profit
        /// </summary>
        /// <value>Profit</value>
        [JsonProperty("profit")]
        public string Profit { get; private set; }

        /// <summary>
        /// Fee deducted
        /// </summary>
        /// <value>Fee deducted</value>
        [JsonProperty("fee")]
        public string Fee { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DeliverySettlement {\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  Leverage: ").Append(Leverage).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Margin: ").Append(Margin).Append("\n");
            sb.Append("  EntryPrice: ").Append(EntryPrice).Append("\n");
            sb.Append("  SettlePrice: ").Append(SettlePrice).Append("\n");
            sb.Append("  Profit: ").Append(Profit).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
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
            return this.Equals(input as DeliverySettlement);
        }

        /// <summary>
        /// Returns true if DeliverySettlement instances are equal
        /// </summary>
        /// <param name="input">Instance of DeliverySettlement to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DeliverySettlement input)
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
                    this.Leverage == input.Leverage ||
                    (this.Leverage != null &&
                    this.Leverage.Equals(input.Leverage))
                ) && 
                (
                    this.Size == input.Size ||
                    this.Size.Equals(input.Size)
                ) && 
                (
                    this.Margin == input.Margin ||
                    (this.Margin != null &&
                    this.Margin.Equals(input.Margin))
                ) && 
                (
                    this.EntryPrice == input.EntryPrice ||
                    (this.EntryPrice != null &&
                    this.EntryPrice.Equals(input.EntryPrice))
                ) && 
                (
                    this.SettlePrice == input.SettlePrice ||
                    (this.SettlePrice != null &&
                    this.SettlePrice.Equals(input.SettlePrice))
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
                if (this.Leverage != null)
                    hashCode = hashCode * 59 + this.Leverage.GetHashCode();
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Margin != null)
                    hashCode = hashCode * 59 + this.Margin.GetHashCode();
                if (this.EntryPrice != null)
                    hashCode = hashCode * 59 + this.EntryPrice.GetHashCode();
                if (this.SettlePrice != null)
                    hashCode = hashCode * 59 + this.SettlePrice.GetHashCode();
                if (this.Profit != null)
                    hashCode = hashCode * 59 + this.Profit.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                return hashCode;
            }
        }
    }

}
