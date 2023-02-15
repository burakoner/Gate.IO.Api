namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// FuturesOrderAmendment
    /// </summary>
    
    public class FuturesOrderAmendment :  IEquatable<FuturesOrderAmendment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesOrderAmendment" /> class.
        /// </summary>
        /// <param name="size">New order size, including filled part.  - If new size is less than or equal to filled size, the order will be cancelled. - Order side must be identical to the original one. - Close order size cannot be changed. - For reduce only orders, increasing size may leads to other reduce only orders being cancelled. - If price is not changed, decreasing size will not change its precedence in order book, while increasing will move it to the last at current price..</param>
        /// <param name="price">New order price..</param>
        public FuturesOrderAmendment(long size = default(long), string price = default(string))
        {
            this.Size = size;
            this.Price = price;
        }

        /// <summary>
        /// New order size, including filled part.  - If new size is less than or equal to filled size, the order will be cancelled. - Order side must be identical to the original one. - Close order size cannot be changed. - For reduce only orders, increasing size may leads to other reduce only orders being cancelled. - If price is not changed, decreasing size will not change its precedence in order book, while increasing will move it to the last at current price.
        /// </summary>
        /// <value>New order size, including filled part.  - If new size is less than or equal to filled size, the order will be cancelled. - Order side must be identical to the original one. - Close order size cannot be changed. - For reduce only orders, increasing size may leads to other reduce only orders being cancelled. - If price is not changed, decreasing size will not change its precedence in order book, while increasing will move it to the last at current price.</value>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// New order price.
        /// </summary>
        /// <value>New order price.</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesOrderAmendment {\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
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
            return this.Equals(input as FuturesOrderAmendment);
        }

        /// <summary>
        /// Returns true if FuturesOrderAmendment instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesOrderAmendment to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesOrderAmendment input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Size == input.Size ||
                    this.Size.Equals(input.Size)
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
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
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                return hashCode;
            }
        }
    }

}
