namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Options contract detail
    /// </summary>
    
    public class OptionsTicker :  IEquatable<OptionsTicker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsTicker" /> class.
        /// </summary>
        /// <param name="name">Options contract name.</param>
        /// <param name="lastPrice">Last trading price (quote currency).</param>
        /// <param name="markPrice">Current mark price (quote currency).</param>
        /// <param name="indexPrice">Current index price (quote currency).</param>
        /// <param name="ask1Size">Best ask size.</param>
        /// <param name="ask1Price">Best ask price.</param>
        /// <param name="bid1Size">Best bid size.</param>
        /// <param name="bid1Price">Best bid price.</param>
        /// <param name="positionSize">Current total long position size.</param>
        /// <param name="markIv">Implied volatility.</param>
        /// <param name="bidIv">Bid side implied volatility.</param>
        /// <param name="askIv">Ask side implied volatility.</param>
        /// <param name="leverage">Current leverage. Formula: underlying_price / mark_price * delta.</param>
        /// <param name="delta">Delta.</param>
        /// <param name="gamma">Gamma.</param>
        /// <param name="vega">Vega.</param>
        /// <param name="theta">Theta.</param>
        /// <param name="rho">Rho.</param>
        public OptionsTicker(string name = default(string), string lastPrice = default(string), string markPrice = default(string), string indexPrice = default(string), long ask1Size = default(long), string ask1Price = default(string), long bid1Size = default(long), string bid1Price = default(string), long positionSize = default(long), string markIv = default(string), string bidIv = default(string), string askIv = default(string), string leverage = default(string), string delta = default(string), string gamma = default(string), string vega = default(string), string theta = default(string), string rho = default(string))
        {
            this.Name = name;
            this.LastPrice = lastPrice;
            this.MarkPrice = markPrice;
            this.IndexPrice = indexPrice;
            this.Ask1Size = ask1Size;
            this.Ask1Price = ask1Price;
            this.Bid1Size = bid1Size;
            this.Bid1Price = bid1Price;
            this.PositionSize = positionSize;
            this.MarkIv = markIv;
            this.BidIv = bidIv;
            this.AskIv = askIv;
            this.Leverage = leverage;
            this.Delta = delta;
            this.Gamma = gamma;
            this.Vega = vega;
            this.Theta = theta;
            this.Rho = rho;
        }

        /// <summary>
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Last trading price (quote currency)
        /// </summary>
        /// <value>Last trading price (quote currency)</value>
        [JsonProperty("last_price")]
        public string LastPrice { get; set; }

        /// <summary>
        /// Current mark price (quote currency)
        /// </summary>
        /// <value>Current mark price (quote currency)</value>
        [JsonProperty("mark_price")]
        public string MarkPrice { get; set; }

        /// <summary>
        /// Current index price (quote currency)
        /// </summary>
        /// <value>Current index price (quote currency)</value>
        [JsonProperty("index_price")]
        public string IndexPrice { get; set; }

        /// <summary>
        /// Best ask size
        /// </summary>
        /// <value>Best ask size</value>
        [JsonProperty("ask1_size")]
        public long Ask1Size { get; set; }

        /// <summary>
        /// Best ask price
        /// </summary>
        /// <value>Best ask price</value>
        [JsonProperty("ask1_price")]
        public string Ask1Price { get; set; }

        /// <summary>
        /// Best bid size
        /// </summary>
        /// <value>Best bid size</value>
        [JsonProperty("bid1_size")]
        public long Bid1Size { get; set; }

        /// <summary>
        /// Best bid price
        /// </summary>
        /// <value>Best bid price</value>
        [JsonProperty("bid1_price")]
        public string Bid1Price { get; set; }

        /// <summary>
        /// Current total long position size
        /// </summary>
        /// <value>Current total long position size</value>
        [JsonProperty("position_size")]
        public long PositionSize { get; set; }

        /// <summary>
        /// Implied volatility
        /// </summary>
        /// <value>Implied volatility</value>
        [JsonProperty("mark_iv")]
        public string MarkIv { get; set; }

        /// <summary>
        /// Bid side implied volatility
        /// </summary>
        /// <value>Bid side implied volatility</value>
        [JsonProperty("bid_iv")]
        public string BidIv { get; set; }

        /// <summary>
        /// Ask side implied volatility
        /// </summary>
        /// <value>Ask side implied volatility</value>
        [JsonProperty("ask_iv")]
        public string AskIv { get; set; }

        /// <summary>
        /// Current leverage. Formula: underlying_price / mark_price * delta
        /// </summary>
        /// <value>Current leverage. Formula: underlying_price / mark_price * delta</value>
        [JsonProperty("leverage")]
        public string Leverage { get; set; }

        /// <summary>
        /// Delta
        /// </summary>
        /// <value>Delta</value>
        [JsonProperty("delta")]
        public string Delta { get; set; }

        /// <summary>
        /// Gamma
        /// </summary>
        /// <value>Gamma</value>
        [JsonProperty("gamma")]
        public string Gamma { get; set; }

        /// <summary>
        /// Vega
        /// </summary>
        /// <value>Vega</value>
        [JsonProperty("vega")]
        public string Vega { get; set; }

        /// <summary>
        /// Theta
        /// </summary>
        /// <value>Theta</value>
        [JsonProperty("theta")]
        public string Theta { get; set; }

        /// <summary>
        /// Rho
        /// </summary>
        /// <value>Rho</value>
        [JsonProperty("rho")]
        public string Rho { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsTicker {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  LastPrice: ").Append(LastPrice).Append("\n");
            sb.Append("  MarkPrice: ").Append(MarkPrice).Append("\n");
            sb.Append("  IndexPrice: ").Append(IndexPrice).Append("\n");
            sb.Append("  Ask1Size: ").Append(Ask1Size).Append("\n");
            sb.Append("  Ask1Price: ").Append(Ask1Price).Append("\n");
            sb.Append("  Bid1Size: ").Append(Bid1Size).Append("\n");
            sb.Append("  Bid1Price: ").Append(Bid1Price).Append("\n");
            sb.Append("  PositionSize: ").Append(PositionSize).Append("\n");
            sb.Append("  MarkIv: ").Append(MarkIv).Append("\n");
            sb.Append("  BidIv: ").Append(BidIv).Append("\n");
            sb.Append("  AskIv: ").Append(AskIv).Append("\n");
            sb.Append("  Leverage: ").Append(Leverage).Append("\n");
            sb.Append("  Delta: ").Append(Delta).Append("\n");
            sb.Append("  Gamma: ").Append(Gamma).Append("\n");
            sb.Append("  Vega: ").Append(Vega).Append("\n");
            sb.Append("  Theta: ").Append(Theta).Append("\n");
            sb.Append("  Rho: ").Append(Rho).Append("\n");
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
            return this.Equals(input as OptionsTicker);
        }

        /// <summary>
        /// Returns true if OptionsTicker instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsTicker to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsTicker input)
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
                    this.LastPrice == input.LastPrice ||
                    (this.LastPrice != null &&
                    this.LastPrice.Equals(input.LastPrice))
                ) && 
                (
                    this.MarkPrice == input.MarkPrice ||
                    (this.MarkPrice != null &&
                    this.MarkPrice.Equals(input.MarkPrice))
                ) && 
                (
                    this.IndexPrice == input.IndexPrice ||
                    (this.IndexPrice != null &&
                    this.IndexPrice.Equals(input.IndexPrice))
                ) && 
                (
                    this.Ask1Size == input.Ask1Size ||
                    this.Ask1Size.Equals(input.Ask1Size)
                ) && 
                (
                    this.Ask1Price == input.Ask1Price ||
                    (this.Ask1Price != null &&
                    this.Ask1Price.Equals(input.Ask1Price))
                ) && 
                (
                    this.Bid1Size == input.Bid1Size ||
                    this.Bid1Size.Equals(input.Bid1Size)
                ) && 
                (
                    this.Bid1Price == input.Bid1Price ||
                    (this.Bid1Price != null &&
                    this.Bid1Price.Equals(input.Bid1Price))
                ) && 
                (
                    this.PositionSize == input.PositionSize ||
                    this.PositionSize.Equals(input.PositionSize)
                ) && 
                (
                    this.MarkIv == input.MarkIv ||
                    (this.MarkIv != null &&
                    this.MarkIv.Equals(input.MarkIv))
                ) && 
                (
                    this.BidIv == input.BidIv ||
                    (this.BidIv != null &&
                    this.BidIv.Equals(input.BidIv))
                ) && 
                (
                    this.AskIv == input.AskIv ||
                    (this.AskIv != null &&
                    this.AskIv.Equals(input.AskIv))
                ) && 
                (
                    this.Leverage == input.Leverage ||
                    (this.Leverage != null &&
                    this.Leverage.Equals(input.Leverage))
                ) && 
                (
                    this.Delta == input.Delta ||
                    (this.Delta != null &&
                    this.Delta.Equals(input.Delta))
                ) && 
                (
                    this.Gamma == input.Gamma ||
                    (this.Gamma != null &&
                    this.Gamma.Equals(input.Gamma))
                ) && 
                (
                    this.Vega == input.Vega ||
                    (this.Vega != null &&
                    this.Vega.Equals(input.Vega))
                ) && 
                (
                    this.Theta == input.Theta ||
                    (this.Theta != null &&
                    this.Theta.Equals(input.Theta))
                ) && 
                (
                    this.Rho == input.Rho ||
                    (this.Rho != null &&
                    this.Rho.Equals(input.Rho))
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
                if (this.LastPrice != null)
                    hashCode = hashCode * 59 + this.LastPrice.GetHashCode();
                if (this.MarkPrice != null)
                    hashCode = hashCode * 59 + this.MarkPrice.GetHashCode();
                if (this.IndexPrice != null)
                    hashCode = hashCode * 59 + this.IndexPrice.GetHashCode();
                hashCode = hashCode * 59 + this.Ask1Size.GetHashCode();
                if (this.Ask1Price != null)
                    hashCode = hashCode * 59 + this.Ask1Price.GetHashCode();
                hashCode = hashCode * 59 + this.Bid1Size.GetHashCode();
                if (this.Bid1Price != null)
                    hashCode = hashCode * 59 + this.Bid1Price.GetHashCode();
                hashCode = hashCode * 59 + this.PositionSize.GetHashCode();
                if (this.MarkIv != null)
                    hashCode = hashCode * 59 + this.MarkIv.GetHashCode();
                if (this.BidIv != null)
                    hashCode = hashCode * 59 + this.BidIv.GetHashCode();
                if (this.AskIv != null)
                    hashCode = hashCode * 59 + this.AskIv.GetHashCode();
                if (this.Leverage != null)
                    hashCode = hashCode * 59 + this.Leverage.GetHashCode();
                if (this.Delta != null)
                    hashCode = hashCode * 59 + this.Delta.GetHashCode();
                if (this.Gamma != null)
                    hashCode = hashCode * 59 + this.Gamma.GetHashCode();
                if (this.Vega != null)
                    hashCode = hashCode * 59 + this.Vega.GetHashCode();
                if (this.Theta != null)
                    hashCode = hashCode * 59 + this.Theta.GetHashCode();
                if (this.Rho != null)
                    hashCode = hashCode * 59 + this.Rho.GetHashCode();
                return hashCode;
            }
        }
    }

}
