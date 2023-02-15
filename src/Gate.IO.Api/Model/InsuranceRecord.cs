namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// InsuranceRecord
    /// </summary>
    
    public class InsuranceRecord :  IEquatable<InsuranceRecord>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceRecord" /> class.
        /// </summary>
        /// <param name="t">Unix timestamp in seconds.</param>
        /// <param name="b">Insurance balance.</param>
        public InsuranceRecord(long t = default(long), string b = default(string))
        {
            this.T = t;
            this.B = b;
        }

        /// <summary>
        /// Unix timestamp in seconds
        /// </summary>
        /// <value>Unix timestamp in seconds</value>
        [JsonProperty("t")]
        public long T { get; set; }

        /// <summary>
        /// Insurance balance
        /// </summary>
        /// <value>Insurance balance</value>
        [JsonProperty("b")]
        public string B { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InsuranceRecord {\n");
            sb.Append("  T: ").Append(T).Append("\n");
            sb.Append("  B: ").Append(B).Append("\n");
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
            return this.Equals(input as InsuranceRecord);
        }

        /// <summary>
        /// Returns true if InsuranceRecord instances are equal
        /// </summary>
        /// <param name="input">Instance of InsuranceRecord to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InsuranceRecord input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.T == input.T ||
                    this.T.Equals(input.T)
                ) && 
                (
                    this.B == input.B ||
                    (this.B != null &&
                    this.B.Equals(input.B))
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
                hashCode = hashCode * 59 + this.T.GetHashCode();
                if (this.B != null)
                    hashCode = hashCode * 59 + this.B.GetHashCode();
                return hashCode;
            }
        }
    }

}
