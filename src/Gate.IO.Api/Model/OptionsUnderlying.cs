namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsUnderlying
    /// </summary>
    
    public class OptionsUnderlying :  IEquatable<OptionsUnderlying>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsUnderlying" /> class.
        /// </summary>
        /// <param name="name">Underlying name.</param>
        /// <param name="indexPrice">Spot index price (quote currency).</param>
        public OptionsUnderlying(string name = default(string), string indexPrice = default(string))
        {
            this.Name = name;
            this.IndexPrice = indexPrice;
        }

        /// <summary>
        /// Underlying name
        /// </summary>
        /// <value>Underlying name</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Spot index price (quote currency)
        /// </summary>
        /// <value>Spot index price (quote currency)</value>
        [JsonProperty("index_price")]
        public string IndexPrice { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsUnderlying {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  IndexPrice: ").Append(IndexPrice).Append("\n");
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
            return this.Equals(input as OptionsUnderlying);
        }

        /// <summary>
        /// Returns true if OptionsUnderlying instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsUnderlying to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsUnderlying input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.IndexPrice == input.IndexPrice ||
                    (this.IndexPrice != null &&
                    this.IndexPrice.Equals(input.IndexPrice))
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
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.IndexPrice != null)
                    hashCode = hashCode * 59 + this.IndexPrice.GetHashCode();
                return hashCode;
            }
        }
    }

}
