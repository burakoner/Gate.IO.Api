namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Statistical data
    /// </summary>
    
    public class FuturesAccountHistory :  IEquatable<FuturesAccountHistory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesAccountHistory" /> class.
        /// </summary>
        /// <param name="dnw">total amount of deposit and withdraw.</param>
        /// <param name="pnl">total amount of trading profit and loss.</param>
        /// <param name="fee">total amount of fee.</param>
        /// <param name="refr">total amount of referrer rebates.</param>
        /// <param name="fund">total amount of funding costs.</param>
        /// <param name="pointDnw">total amount of point deposit and withdraw.</param>
        /// <param name="pointFee">total amount of point fee.</param>
        /// <param name="pointRefr">total amount of referrer rebates of point fee.</param>
        /// <param name="bonusDnw">total amount of perpetual contract bonus transfer.</param>
        /// <param name="bonusOffset">total amount of perpetual contract bonus deduction.</param>
        public FuturesAccountHistory(string dnw = default(string), string pnl = default(string), string fee = default(string), string refr = default(string), string fund = default(string), string pointDnw = default(string), string pointFee = default(string), string pointRefr = default(string), string bonusDnw = default(string), string bonusOffset = default(string))
        {
            this.Dnw = dnw;
            this.Pnl = pnl;
            this.Fee = fee;
            this.Refr = refr;
            this.Fund = fund;
            this.PointDnw = pointDnw;
            this.PointFee = pointFee;
            this.PointRefr = pointRefr;
            this.BonusDnw = bonusDnw;
            this.BonusOffset = bonusOffset;
        }

        /// <summary>
        /// total amount of deposit and withdraw
        /// </summary>
        /// <value>total amount of deposit and withdraw</value>
        [JsonProperty("dnw")]
        public string Dnw { get; set; }

        /// <summary>
        /// total amount of trading profit and loss
        /// </summary>
        /// <value>total amount of trading profit and loss</value>
        [JsonProperty("pnl")]
        public string Pnl { get; set; }

        /// <summary>
        /// total amount of fee
        /// </summary>
        /// <value>total amount of fee</value>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// total amount of referrer rebates
        /// </summary>
        /// <value>total amount of referrer rebates</value>
        [JsonProperty("refr")]
        public string Refr { get; set; }

        /// <summary>
        /// total amount of funding costs
        /// </summary>
        /// <value>total amount of funding costs</value>
        [JsonProperty("fund")]
        public string Fund { get; set; }

        /// <summary>
        /// total amount of point deposit and withdraw
        /// </summary>
        /// <value>total amount of point deposit and withdraw</value>
        [JsonProperty("point_dnw")]
        public string PointDnw { get; set; }

        /// <summary>
        /// total amount of point fee
        /// </summary>
        /// <value>total amount of point fee</value>
        [JsonProperty("point_fee")]
        public string PointFee { get; set; }

        /// <summary>
        /// total amount of referrer rebates of point fee
        /// </summary>
        /// <value>total amount of referrer rebates of point fee</value>
        [JsonProperty("point_refr")]
        public string PointRefr { get; set; }

        /// <summary>
        /// total amount of perpetual contract bonus transfer
        /// </summary>
        /// <value>total amount of perpetual contract bonus transfer</value>
        [JsonProperty("bonus_dnw")]
        public string BonusDnw { get; set; }

        /// <summary>
        /// total amount of perpetual contract bonus deduction
        /// </summary>
        /// <value>total amount of perpetual contract bonus deduction</value>
        [JsonProperty("bonus_offset")]
        public string BonusOffset { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesAccountHistory {\n");
            sb.Append("  Dnw: ").Append(Dnw).Append("\n");
            sb.Append("  Pnl: ").Append(Pnl).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  Refr: ").Append(Refr).Append("\n");
            sb.Append("  Fund: ").Append(Fund).Append("\n");
            sb.Append("  PointDnw: ").Append(PointDnw).Append("\n");
            sb.Append("  PointFee: ").Append(PointFee).Append("\n");
            sb.Append("  PointRefr: ").Append(PointRefr).Append("\n");
            sb.Append("  BonusDnw: ").Append(BonusDnw).Append("\n");
            sb.Append("  BonusOffset: ").Append(BonusOffset).Append("\n");
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
            return this.Equals(input as FuturesAccountHistory);
        }

        /// <summary>
        /// Returns true if FuturesAccountHistory instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesAccountHistory to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesAccountHistory input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Dnw == input.Dnw ||
                    (this.Dnw != null &&
                    this.Dnw.Equals(input.Dnw))
                ) && 
                (
                    this.Pnl == input.Pnl ||
                    (this.Pnl != null &&
                    this.Pnl.Equals(input.Pnl))
                ) && 
                (
                    this.Fee == input.Fee ||
                    (this.Fee != null &&
                    this.Fee.Equals(input.Fee))
                ) && 
                (
                    this.Refr == input.Refr ||
                    (this.Refr != null &&
                    this.Refr.Equals(input.Refr))
                ) && 
                (
                    this.Fund == input.Fund ||
                    (this.Fund != null &&
                    this.Fund.Equals(input.Fund))
                ) && 
                (
                    this.PointDnw == input.PointDnw ||
                    (this.PointDnw != null &&
                    this.PointDnw.Equals(input.PointDnw))
                ) && 
                (
                    this.PointFee == input.PointFee ||
                    (this.PointFee != null &&
                    this.PointFee.Equals(input.PointFee))
                ) && 
                (
                    this.PointRefr == input.PointRefr ||
                    (this.PointRefr != null &&
                    this.PointRefr.Equals(input.PointRefr))
                ) && 
                (
                    this.BonusDnw == input.BonusDnw ||
                    (this.BonusDnw != null &&
                    this.BonusDnw.Equals(input.BonusDnw))
                ) && 
                (
                    this.BonusOffset == input.BonusOffset ||
                    (this.BonusOffset != null &&
                    this.BonusOffset.Equals(input.BonusOffset))
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
                if (this.Dnw != null)
                    hashCode = hashCode * 59 + this.Dnw.GetHashCode();
                if (this.Pnl != null)
                    hashCode = hashCode * 59 + this.Pnl.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                if (this.Refr != null)
                    hashCode = hashCode * 59 + this.Refr.GetHashCode();
                if (this.Fund != null)
                    hashCode = hashCode * 59 + this.Fund.GetHashCode();
                if (this.PointDnw != null)
                    hashCode = hashCode * 59 + this.PointDnw.GetHashCode();
                if (this.PointFee != null)
                    hashCode = hashCode * 59 + this.PointFee.GetHashCode();
                if (this.PointRefr != null)
                    hashCode = hashCode * 59 + this.PointRefr.GetHashCode();
                if (this.BonusDnw != null)
                    hashCode = hashCode * 59 + this.BonusDnw.GetHashCode();
                if (this.BonusOffset != null)
                    hashCode = hashCode * 59 + this.BonusOffset.GetHashCode();
                return hashCode;
            }
        }
    }

}
