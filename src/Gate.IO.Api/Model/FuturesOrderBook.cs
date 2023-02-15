namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// FuturesOrderBook
    /// </summary>
    
    public class FuturesOrderBook :  IEquatable<FuturesOrderBook>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesOrderBook" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected FuturesOrderBook() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesOrderBook" /> class.
        /// </summary>
        /// <param name="id">Order Book ID. Increases by 1 on every order book change. Set &#x60;with_id&#x3D;true&#x60; to include this field in response.</param>
        /// <param name="current">Response data generation timestamp.</param>
        /// <param name="update">Order book changed timestamp.</param>
        /// <param name="asks">Asks order depth (required).</param>
        /// <param name="bids">Bids order depth (required).</param>
        public FuturesOrderBook(long id = default(long), double current = default(double), double update = default(double), List<FuturesOrderBookItem> asks = default(List<FuturesOrderBookItem>), List<FuturesOrderBookItem> bids = default(List<FuturesOrderBookItem>))
        {
            // to ensure "asks" is required (not null)
            this.Asks = asks ?? throw new ArgumentNullException("asks", "asks is a required property for FuturesOrderBook and cannot be null");
            // to ensure "bids" is required (not null)
            this.Bids = bids ?? throw new ArgumentNullException("bids", "bids is a required property for FuturesOrderBook and cannot be null");
            this.Id = id;
            this.Current = current;
            this.Update = update;
        }

        /// <summary>
        /// Order Book ID. Increases by 1 on every order book change. Set &#x60;with_id&#x3D;true&#x60; to include this field in response
        /// </summary>
        /// <value>Order Book ID. Increases by 1 on every order book change. Set &#x60;with_id&#x3D;true&#x60; to include this field in response</value>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Response data generation timestamp
        /// </summary>
        /// <value>Response data generation timestamp</value>
        [JsonProperty("current")]
        public double Current { get; set; }

        /// <summary>
        /// Order book changed timestamp
        /// </summary>
        /// <value>Order book changed timestamp</value>
        [JsonProperty("update")]
        public double Update { get; set; }

        /// <summary>
        /// Asks order depth
        /// </summary>
        /// <value>Asks order depth</value>
        [JsonProperty("asks")]
        public List<FuturesOrderBookItem> Asks { get; set; }

        /// <summary>
        /// Bids order depth
        /// </summary>
        /// <value>Bids order depth</value>
        [JsonProperty("bids")]
        public List<FuturesOrderBookItem> Bids { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesOrderBook {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Current: ").Append(Current).Append("\n");
            sb.Append("  Update: ").Append(Update).Append("\n");
            sb.Append("  Asks: ").Append(Asks).Append("\n");
            sb.Append("  Bids: ").Append(Bids).Append("\n");
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
            return this.Equals(input as FuturesOrderBook);
        }

        /// <summary>
        /// Returns true if FuturesOrderBook instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesOrderBook to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesOrderBook input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.Current == input.Current ||
                    this.Current.Equals(input.Current)
                ) && 
                (
                    this.Update == input.Update ||
                    this.Update.Equals(input.Update)
                ) && 
                (
                    this.Asks == input.Asks ||
                    this.Asks != null &&
                    input.Asks != null &&
                    this.Asks.SequenceEqual(input.Asks)
                ) && 
                (
                    this.Bids == input.Bids ||
                    this.Bids != null &&
                    input.Bids != null &&
                    this.Bids.SequenceEqual(input.Bids)
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
                hashCode = hashCode * 59 + this.Current.GetHashCode();
                hashCode = hashCode * 59 + this.Update.GetHashCode();
                if (this.Asks != null)
                    hashCode = hashCode * 59 + this.Asks.GetHashCode();
                if (this.Bids != null)
                    hashCode = hashCode * 59 + this.Bids.GetHashCode();
                return hashCode;
            }
        }
    }

}
