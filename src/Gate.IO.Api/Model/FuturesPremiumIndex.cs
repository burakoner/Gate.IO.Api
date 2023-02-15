namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// data point in every timestamp
    /// </summary>
    
    public class FuturesPremiumIndex :  IEquatable<FuturesPremiumIndex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesPremiumIndex" /> class.
        /// </summary>
        /// <param name="t">Unix timestamp in seconds.</param>
        /// <param name="c">Close price.</param>
        /// <param name="h">Highest price.</param>
        /// <param name="l">Lowest price&#x60;.</param>
        /// <param name="o">Open price.</param>
        public FuturesPremiumIndex(double t = default(double), string c = default(string), string h = default(string), string l = default(string), string o = default(string))
        {
            this.T = t;
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
        /// Close price
        /// </summary>
        /// <value>Close price</value>
        [JsonProperty("c")]
        public string C { get; set; }

        /// <summary>
        /// Highest price
        /// </summary>
        /// <value>Highest price</value>
        [JsonProperty("h")]
        public string H { get; set; }

        /// <summary>
        /// Lowest price&#x60;
        /// </summary>
        /// <value>Lowest price&#x60;</value>
        [JsonProperty("l")]
        public string L { get; set; }

        /// <summary>
        /// Open price
        /// </summary>
        /// <value>Open price</value>
        [JsonProperty("o")]
        public string O { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesPremiumIndex {\n");
            sb.Append("  T: ").Append(T).Append("\n");
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
            return this.Equals(input as FuturesPremiumIndex);
        }

        /// <summary>
        /// Returns true if FuturesPremiumIndex instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesPremiumIndex to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesPremiumIndex input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.T == input.T ||
                    this.T.Equals(input.T)
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
