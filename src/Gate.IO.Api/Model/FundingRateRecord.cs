namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// FundingRateRecord
    /// </summary>
    
    public class FundingRateRecord :  IEquatable<FundingRateRecord>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundingRateRecord" /> class.
        /// </summary>
        /// <param name="t">Unix timestamp in seconds.</param>
        /// <param name="r">Funding rate.</param>
        public FundingRateRecord(long t = default(long), string r = default(string))
        {
            this.T = t;
            this.R = r;
        }

        /// <summary>
        /// Unix timestamp in seconds
        /// </summary>
        /// <value>Unix timestamp in seconds</value>
        [JsonProperty("t")]
        public long T { get; set; }

        /// <summary>
        /// Funding rate
        /// </summary>
        /// <value>Funding rate</value>
        [JsonProperty("r")]
        public string R { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FundingRateRecord {\n");
            sb.Append("  T: ").Append(T).Append("\n");
            sb.Append("  R: ").Append(R).Append("\n");
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
            return this.Equals(input as FundingRateRecord);
        }

        /// <summary>
        /// Returns true if FundingRateRecord instances are equal
        /// </summary>
        /// <param name="input">Instance of FundingRateRecord to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FundingRateRecord input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.T == input.T ||
                    this.T.Equals(input.T)
                ) && 
                (
                    this.R == input.R ||
                    (this.R != null &&
                    this.R.Equals(input.R))
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
                if (this.R != null)
                    hashCode = hashCode * 59 + this.R.GetHashCode();
                return hashCode;
            }
        }
    }

}
