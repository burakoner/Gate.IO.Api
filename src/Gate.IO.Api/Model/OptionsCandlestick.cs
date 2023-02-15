namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// data point in every timestamp
    /// </summary>
    
    public class OptionsCandlestick :  IEquatable<OptionsCandlestick>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsCandlestick" /> class.
        /// </summary>
        /// <param name="t">Unix timestamp in seconds.</param>
        /// <param name="v">size volume (contract size). Only returned if &#x60;contract&#x60; is not prefixed.</param>
        /// <param name="c">Close price (quote currency, unit: underlying corresponding option price).</param>
        /// <param name="h">Highest price (quote currency, unit: underlying corresponding option price).</param>
        /// <param name="l">Lowest price (quote currency, unit: underlying corresponding option price).</param>
        /// <param name="o">Open price (quote currency, unit: underlying corresponding option price).</param>
        public OptionsCandlestick(double t = default(double), long v = default(long), string c = default(string), string h = default(string), string l = default(string), string o = default(string))
        {
            this.T = t;
            this.V = v;
            this.C = c;
            this.H = h;
            this.L = l;
            this.O = o;
        }

        /// <summary>
        /// Unix timestamp in seconds
        /// </summary>
        /// <value>Unix timestamp in seconds</value>
        [JsonProperty("t")]
        public double T { get; set; }

        /// <summary>
        /// size volume (contract size). Only returned if &#x60;contract&#x60; is not prefixed
        /// </summary>
        /// <value>size volume (contract size). Only returned if &#x60;contract&#x60; is not prefixed</value>
        [JsonProperty("v")]
        public long V { get; set; }

        /// <summary>
        /// Close price (quote currency, unit: underlying corresponding option price)
        /// </summary>
        /// <value>Close price (quote currency, unit: underlying corresponding option price)</value>
        [JsonProperty("c")]
        public string C { get; set; }

        /// <summary>
        /// Highest price (quote currency, unit: underlying corresponding option price)
        /// </summary>
        /// <value>Highest price (quote currency, unit: underlying corresponding option price)</value>
        [JsonProperty("h")]
        public string H { get; set; }

        /// <summary>
        /// Lowest price (quote currency, unit: underlying corresponding option price)
        /// </summary>
        /// <value>Lowest price (quote currency, unit: underlying corresponding option price)</value>
        [JsonProperty("l")]
        public string L { get; set; }

        /// <summary>
        /// Open price (quote currency, unit: underlying corresponding option price)
        /// </summary>
        /// <value>Open price (quote currency, unit: underlying corresponding option price)</value>
        [JsonProperty("o")]
        public string O { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsCandlestick {\n");
            sb.Append("  T: ").Append(T).Append("\n");
            sb.Append("  V: ").Append(V).Append("\n");
            sb.Append("  C: ").Append(C).Append("\n");
            sb.Append("  H: ").Append(H).Append("\n");
            sb.Append("  L: ").Append(L).Append("\n");
            sb.Append("  O: ").Append(O).Append("\n");
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
            return this.Equals(input as OptionsCandlestick);
        }

        /// <summary>
        /// Returns true if OptionsCandlestick instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsCandlestick to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsCandlestick input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.T == input.T ||
                    this.T.Equals(input.T)
                ) && 
                (
                    this.V == input.V ||
                    this.V.Equals(input.V)
                ) && 
                (
                    this.C == input.C ||
                    (this.C != null &&
                    this.C.Equals(input.C))
                ) && 
                (
                    this.H == input.H ||
                    (this.H != null &&
                    this.H.Equals(input.H))
                ) && 
                (
                    this.L == input.L ||
                    (this.L != null &&
                    this.L.Equals(input.L))
                ) && 
                (
                    this.O == input.O ||
                    (this.O != null &&
                    this.O.Equals(input.O))
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
                hashCode = hashCode * 59 + this.V.GetHashCode();
                if (this.C != null)
                    hashCode = hashCode * 59 + this.C.GetHashCode();
                if (this.H != null)
                    hashCode = hashCode * 59 + this.H.GetHashCode();
                if (this.L != null)
                    hashCode = hashCode * 59 + this.L.GetHashCode();
                if (this.O != null)
                    hashCode = hashCode * 59 + this.O.GetHashCode();
                return hashCode;
            }
        }
    }

}
