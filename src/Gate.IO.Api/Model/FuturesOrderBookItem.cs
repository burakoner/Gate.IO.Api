namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// FuturesOrderBookItem
    /// </summary>
    
    public class FuturesOrderBookItem :  IEquatable<FuturesOrderBookItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesOrderBookItem" /> class.
        /// </summary>
        /// <param name="p">Price (quote currency).</param>
        /// <param name="s">Size.</param>
        public FuturesOrderBookItem(string p = default(string), long s = default(long))
        {
            this.P = p;
            this.S = s;
        }

        /// <summary>
        /// Price (quote currency)
        /// </summary>
        /// <value>Price (quote currency)</value>
        [JsonProperty("p")]
        public string P { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        /// <value>Size</value>
        [JsonProperty("s")]
        public long S { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesOrderBookItem {\n");
            sb.Append("  P: ").Append(P).Append("\n");
            sb.Append("  S: ").Append(S).Append("\n");
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
            return this.Equals(input as FuturesOrderBookItem);
        }

        /// <summary>
        /// Returns true if FuturesOrderBookItem instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesOrderBookItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesOrderBookItem input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.P == input.P ||
                    (this.P != null &&
                    this.P.Equals(input.P))
                ) && 
                (
                    this.S == input.S ||
                    this.S.Equals(input.S)
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
                if (this.P != null)
                    hashCode = hashCode * 59 + this.P.GetHashCode();
                hashCode = hashCode * 59 + this.S.GetHashCode();
                return hashCode;
            }
        }
    }

}
