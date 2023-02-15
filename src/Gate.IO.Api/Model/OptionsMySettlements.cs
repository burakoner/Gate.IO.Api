namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsMySettlements
    /// </summary>
    
    public class OptionsMySettlements :  IEquatable<OptionsMySettlements>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMySettlements" /> class.
        /// </summary>
        /// <param name="time">Settlement time.</param>
        /// <param name="underlying">Underlying.</param>
        /// <param name="contract">Options contract name.</param>
        /// <param name="strikePrice">Strike price (quote currency).</param>
        /// <param name="settlePrice">Settlement price (quote currency).</param>
        /// <param name="size">Size.</param>
        /// <param name="settleProfit">Settlement profit (quote currency).</param>
        /// <param name="fee">Fee (quote currency).</param>
        /// <param name="realisedPnl">The accumulated profit and loss of opening a position, including premium, fee, settlement profit, etc. (quote currency).</param>
        public OptionsMySettlements(double time = default(double), string underlying = default(string), string contract = default(string), string strikePrice = default(string), string settlePrice = default(string), long size = default(long), string settleProfit = default(string), string fee = default(string), string realisedPnl = default(string))
        {
            this.Time = time;
            this.Underlying = underlying;
            this.Contract = contract;
            this.StrikePrice = strikePrice;
            this.SettlePrice = settlePrice;
            this.Size = size;
            this.SettleProfit = settleProfit;
            this.Fee = fee;
            this.RealisedPnl = realisedPnl;
        }

        /// <summary>
        /// Settlement time
        /// </summary>
        /// <value>Settlement time</value>
        [JsonProperty("time")]
        public double Time { get; set; }

        /// <summary>
        /// Underlying
        /// </summary>
        /// <value>Underlying</value>
        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        /// <summary>
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("contract")]
        public string Contract { get; set; }

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
        /// Size
        /// </summary>
        /// <value>Size</value>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// Settlement profit (quote currency)
        /// </summary>
        /// <value>Settlement profit (quote currency)</value>
        [JsonProperty("settle_profit")]
        public string SettleProfit { get; set; }

        /// <summary>
        /// Fee (quote currency)
        /// </summary>
        /// <value>Fee (quote currency)</value>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// The accumulated profit and loss of opening a position, including premium, fee, settlement profit, etc. (quote currency)
        /// </summary>
        /// <value>The accumulated profit and loss of opening a position, including premium, fee, settlement profit, etc. (quote currency)</value>
        [JsonProperty("realised_pnl")]
        public string RealisedPnl { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsMySettlements {\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  Underlying: ").Append(Underlying).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  StrikePrice: ").Append(StrikePrice).Append("\n");
            sb.Append("  SettlePrice: ").Append(SettlePrice).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  SettleProfit: ").Append(SettleProfit).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  RealisedPnl: ").Append(RealisedPnl).Append("\n");
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
            return this.Equals(input as OptionsMySettlements);
        }

        /// <summary>
        /// Returns true if OptionsMySettlements instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsMySettlements to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsMySettlements input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Time == input.Time ||
                    this.Time.Equals(input.Time)
                ) && 
                (
                    this.Underlying == input.Underlying ||
                    (this.Underlying != null &&
                    this.Underlying.Equals(input.Underlying))
                ) && 
                (
                    this.Contract == input.Contract ||
                    (this.Contract != null &&
                    this.Contract.Equals(input.Contract))
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
                ) && 
                (
                    this.Size == input.Size ||
                    this.Size.Equals(input.Size)
                ) && 
                (
                    this.SettleProfit == input.SettleProfit ||
                    (this.SettleProfit != null &&
                    this.SettleProfit.Equals(input.SettleProfit))
                ) && 
                (
                    this.Fee == input.Fee ||
                    (this.Fee != null &&
                    this.Fee.Equals(input.Fee))
                ) && 
                (
                    this.RealisedPnl == input.RealisedPnl ||
                    (this.RealisedPnl != null &&
                    this.RealisedPnl.Equals(input.RealisedPnl))
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
                if (this.Underlying != null)
                    hashCode = hashCode * 59 + this.Underlying.GetHashCode();
                if (this.Contract != null)
                    hashCode = hashCode * 59 + this.Contract.GetHashCode();
                if (this.StrikePrice != null)
                    hashCode = hashCode * 59 + this.StrikePrice.GetHashCode();
                if (this.SettlePrice != null)
                    hashCode = hashCode * 59 + this.SettlePrice.GetHashCode();
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.SettleProfit != null)
                    hashCode = hashCode * 59 + this.SettleProfit.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                if (this.RealisedPnl != null)
                    hashCode = hashCode * 59 + this.RealisedPnl.GetHashCode();
                return hashCode;
            }
        }
    }

}
