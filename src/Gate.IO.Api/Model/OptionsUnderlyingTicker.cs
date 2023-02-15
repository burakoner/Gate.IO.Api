namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Options underlying detail
    /// </summary>
    
    public class OptionsUnderlyingTicker :  IEquatable<OptionsUnderlyingTicker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsUnderlyingTicker" /> class.
        /// </summary>
        /// <param name="tradePut">Total put options trades amount in last 24h.</param>
        /// <param name="tradeCall">Total call options trades amount in last 24h.</param>
        /// <param name="indexPrice">Index price (quote currency).</param>
        public OptionsUnderlyingTicker(long tradePut = default(long), long tradeCall = default(long), string indexPrice = default(string))
        {
            this.TradePut = tradePut;
            this.TradeCall = tradeCall;
            this.IndexPrice = indexPrice;
        }

        /// <summary>
        /// Total put options trades amount in last 24h
        /// </summary>
        /// <value>Total put options trades amount in last 24h</value>
        [JsonProperty("trade_put")]
        public long TradePut { get; set; }

        /// <summary>
        /// Total call options trades amount in last 24h
        /// </summary>
        /// <value>Total call options trades amount in last 24h</value>
        [JsonProperty("trade_call")]
        public long TradeCall { get; set; }

        /// <summary>
        /// Index price (quote currency)
        /// </summary>
        /// <value>Index price (quote currency)</value>
        [JsonProperty("index_price")]
        public string IndexPrice { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsUnderlyingTicker {\n");
            sb.Append("  TradePut: ").Append(TradePut).Append("\n");
            sb.Append("  TradeCall: ").Append(TradeCall).Append("\n");
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
            return this.Equals(input as OptionsUnderlyingTicker);
        }

        /// <summary>
        /// Returns true if OptionsUnderlyingTicker instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsUnderlyingTicker to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsUnderlyingTicker input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.TradePut == input.TradePut ||
                    this.TradePut.Equals(input.TradePut)
                ) && 
                (
                    this.TradeCall == input.TradeCall ||
                    this.TradeCall.Equals(input.TradeCall)
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
                hashCode = hashCode * 59 + this.TradePut.GetHashCode();
                hashCode = hashCode * 59 + this.TradeCall.GetHashCode();
                if (this.IndexPrice != null)
                    hashCode = hashCode * 59 + this.IndexPrice.GetHashCode();
                return hashCode;
            }
        }
    }

}
