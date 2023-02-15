namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Countdown cancel task detail
    /// </summary>
    public class CountdownCancelAllFuturesTask :  IEquatable<CountdownCancelAllFuturesTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownCancelAllFuturesTask" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CountdownCancelAllFuturesTask() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownCancelAllFuturesTask" /> class.
        /// </summary>
        /// <param name="timeout">Countdown time, in seconds  At least 5 seconds, 0 means cancel the countdown (required).</param>
        /// <param name="contract">Futures contract.</param>
        public CountdownCancelAllFuturesTask(int timeout = default(int), string contract = default(string))
        {
            this.Timeout = timeout;
            this.Contract = contract;
        }

        /// <summary>
        /// Countdown time, in seconds  At least 5 seconds, 0 means cancel the countdown
        /// </summary>
        /// <value>Countdown time, in seconds  At least 5 seconds, 0 means cancel the countdown</value>
        [JsonProperty("timeout")]
        public int Timeout { get; set; }

        /// <summary>
        /// Futures contract
        /// </summary>
        /// <value>Futures contract</value>
        [JsonProperty("contract")]
        public string Contract { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CountdownCancelAllFuturesTask {\n");
            sb.Append("  Timeout: ").Append(Timeout).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
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
            return this.Equals(input as CountdownCancelAllFuturesTask);
        }

        /// <summary>
        /// Returns true if CountdownCancelAllFuturesTask instances are equal
        /// </summary>
        /// <param name="input">Instance of CountdownCancelAllFuturesTask to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CountdownCancelAllFuturesTask input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Timeout == input.Timeout ||
                    this.Timeout.Equals(input.Timeout)
                ) && 
                (
                    this.Contract == input.Contract ||
                    (this.Contract != null &&
                    this.Contract.Equals(input.Contract))
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
                hashCode = hashCode * 59 + this.Timeout.GetHashCode();
                if (this.Contract != null)
                    hashCode = hashCode * 59 + this.Contract.GetHashCode();
                return hashCode;
            }
        }
    }

}
