namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// TriggerOrderResponse
    /// </summary>
    
    public class TriggerOrderResponse :  IEquatable<TriggerOrderResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerOrderResponse" /> class.
        /// </summary>
        /// <param name="id">Auto order ID.</param>
        public TriggerOrderResponse(long id = default(long))
        {
            this.Id = id;
        }

        /// <summary>
        /// Auto order ID
        /// </summary>
        /// <value>Auto order ID</value>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TriggerOrderResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
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
            return this.Equals(input as TriggerOrderResponse);
        }

        /// <summary>
        /// Returns true if TriggerOrderResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of TriggerOrderResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TriggerOrderResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
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
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                return hashCode;
            }
        }
    }

}
