namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsPositionClose
    /// </summary>
    
    public class OptionsPositionClose :  IEquatable<OptionsPositionClose>
    {
        /// <summary>
        /// Position side, long or short
        /// </summary>
        /// <value>Position side, long or short</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SideEnum
        {
            /// <summary>
            /// Enum Long for value: long
            /// </summary>
            [EnumMember(Value = "long")]
            Long = 1,

            /// <summary>
            /// Enum Short for value: short
            /// </summary>
            [EnumMember(Value = "short")]
            Short = 2

        }

        /// <summary>
        /// Position side, long or short
        /// </summary>
        /// <value>Position side, long or short</value>
        [JsonProperty("side")]
        public SideEnum? Side { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPositionClose" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public OptionsPositionClose()
        {
        }

        /// <summary>
        /// Position close time
        /// </summary>
        /// <value>Position close time</value>
        [JsonProperty("time")]
        public double Time { get; private set; }

        /// <summary>
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("contract")]
        public string Contract { get; private set; }

        /// <summary>
        /// PNL
        /// </summary>
        /// <value>PNL</value>
        [JsonProperty("pnl")]
        public string Pnl { get; private set; }

        /// <summary>
        /// Text of close order
        /// </summary>
        /// <value>Text of close order</value>
        [JsonProperty("text")]
        public string Text { get; private set; }

        /// <summary>
        /// settlement size
        /// </summary>
        /// <value>settlement size</value>
        [JsonProperty("settle_size")]
        public string SettleSize { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsPositionClose {\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  Side: ").Append(Side).Append("\n");
            sb.Append("  Pnl: ").Append(Pnl).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  SettleSize: ").Append(SettleSize).Append("\n");
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
            return this.Equals(input as OptionsPositionClose);
        }

        /// <summary>
        /// Returns true if OptionsPositionClose instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsPositionClose to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsPositionClose input)
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
                    this.Side == input.Side ||
                    this.Side.Equals(input.Side)
                ) && 
                (
                    this.Pnl == input.Pnl ||
                    (this.Pnl != null &&
                    this.Pnl.Equals(input.Pnl))
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.SettleSize == input.SettleSize ||
                    (this.SettleSize != null &&
                    this.SettleSize.Equals(input.SettleSize))
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
                hashCode = hashCode * 59 + this.Side.GetHashCode();
                if (this.Pnl != null)
                    hashCode = hashCode * 59 + this.Pnl.GetHashCode();
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.SettleSize != null)
                    hashCode = hashCode * 59 + this.SettleSize.GetHashCode();
                return hashCode;
            }
        }
    }

}
