namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// AutoRepaySetting
    /// </summary>
    public class AutoRepaySetting :  IEquatable<AutoRepaySetting>
    {
        /// <summary>
        /// Auto repayment status. &#x60;on&#x60; - enabled, &#x60;off&#x60; - disabled
        /// </summary>
        /// <value>Auto repayment status. &#x60;on&#x60; - enabled, &#x60;off&#x60; - disabled</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum On for value: on
            /// </summary>
            [EnumMember(Value = "on")]
            On = 1,

            /// <summary>
            /// Enum Off for value: off
            /// </summary>
            [EnumMember(Value = "off")]
            Off = 2

        }

        /// <summary>
        /// Auto repayment status. &#x60;on&#x60; - enabled, &#x60;off&#x60; - disabled
        /// </summary>
        /// <value>Auto repayment status. &#x60;on&#x60; - enabled, &#x60;off&#x60; - disabled</value>
        [JsonProperty("status")]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoRepaySetting" /> class.
        /// </summary>
        /// <param name="status">Auto repayment status. &#x60;on&#x60; - enabled, &#x60;off&#x60; - disabled.</param>
        public AutoRepaySetting(StatusEnum? status = default(StatusEnum?))
        {
            this.Status = status;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AutoRepaySetting {\n");
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
            return this.Equals(input as AutoRepaySetting);
        }

        /// <summary>
        /// Returns true if AutoRepaySetting instances are equal
        /// </summary>
        /// <param name="input">Instance of AutoRepaySetting to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AutoRepaySetting input)
        {
            if (input == null)
                return false;

            return 
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
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                return hashCode;
            }
        }
    }

}
