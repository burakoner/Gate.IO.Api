namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Options contract detail
    /// </summary>
    
    public class OptionsContract :  IEquatable<OptionsContract>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsContract" /> class.
        /// </summary>
        /// <param name="name">Options contract name.</param>
        /// <param name="tag">tag.</param>
        /// <param name="createTime">Creation time.</param>
        /// <param name="expirationTime">Expiration time.</param>
        /// <param name="isCall">&#x60;true&#x60; means call options, while &#x60;false&#x60; is put options.</param>
        /// <param name="multiplier">Multiplier used in converting from invoicing to settlement currency.</param>
        /// <param name="underlying">Underlying.</param>
        /// <param name="underlyingPrice">Underlying price (quote currency).</param>
        /// <param name="lastPrice">Last trading price.</param>
        /// <param name="markPrice">Current mark price (quote currency).</param>
        /// <param name="indexPrice">Current index price (quote currency).</param>
        /// <param name="makerFeeRate">Maker fee rate, where negative means rebate.</param>
        /// <param name="takerFeeRate">Taker fee rate.</param>
        /// <param name="orderPriceRound">Minimum order price increment.</param>
        /// <param name="markPriceRound">Minimum mark price increment.</param>
        /// <param name="orderSizeMin">Minimum order size the contract allowed.</param>
        /// <param name="orderSizeMax">Maximum order size the contract allowed.</param>
        /// <param name="orderPriceDeviate">deviation between order price and current index price. If price of an order is denoted as order_price, it must meet the following condition:      abs(order_price - mark_price) &lt;&#x3D; mark_price * order_price_deviate.</param>
        /// <param name="refDiscountRate">Referral fee rate discount.</param>
        /// <param name="refRebateRate">Referrer commission rate.</param>
        /// <param name="orderbookId">Current orderbook ID.</param>
        /// <param name="tradeId">Current trade ID.</param>
        /// <param name="tradeSize">Historical accumulated trade size.</param>
        /// <param name="positionSize">Current total long position size.</param>
        /// <param name="ordersLimit">Maximum number of open orders.</param>
        public OptionsContract(string name = default(string), string tag = default(string), double createTime = default(double), double expirationTime = default(double), bool isCall = default(bool), string multiplier = default(string), string underlying = default(string), string underlyingPrice = default(string), string lastPrice = default(string), string markPrice = default(string), string indexPrice = default(string), string makerFeeRate = default(string), string takerFeeRate = default(string), string orderPriceRound = default(string), string markPriceRound = default(string), long orderSizeMin = default(long), long orderSizeMax = default(long), string orderPriceDeviate = default(string), string refDiscountRate = default(string), string refRebateRate = default(string), long orderbookId = default(long), long tradeId = default(long), long tradeSize = default(long), long positionSize = default(long), int ordersLimit = default(int))
        {
            this.Name = name;
            this.Tag = tag;
            this.CreateTime = createTime;
            this.ExpirationTime = expirationTime;
            this.IsCall = isCall;
            this.Multiplier = multiplier;
            this.Underlying = underlying;
            this.UnderlyingPrice = underlyingPrice;
            this.LastPrice = lastPrice;
            this.MarkPrice = markPrice;
            this.IndexPrice = indexPrice;
            this.MakerFeeRate = makerFeeRate;
            this.TakerFeeRate = takerFeeRate;
            this.OrderPriceRound = orderPriceRound;
            this.MarkPriceRound = markPriceRound;
            this.OrderSizeMin = orderSizeMin;
            this.OrderSizeMax = orderSizeMax;
            this.OrderPriceDeviate = orderPriceDeviate;
            this.RefDiscountRate = refDiscountRate;
            this.RefRebateRate = refRebateRate;
            this.OrderbookId = orderbookId;
            this.TradeId = tradeId;
            this.TradeSize = tradeSize;
            this.PositionSize = positionSize;
            this.OrdersLimit = ordersLimit;
        }

        /// <summary>
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// tag
        /// </summary>
        /// <value>tag</value>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        /// <value>Creation time</value>
        [JsonProperty("create_time")]
        public double CreateTime { get; set; }

        /// <summary>
        /// Expiration time
        /// </summary>
        /// <value>Expiration time</value>
        [JsonProperty("expiration_time")]
        public double ExpirationTime { get; set; }

        /// <summary>
        /// &#x60;true&#x60; means call options, while &#x60;false&#x60; is put options
        /// </summary>
        /// <value>&#x60;true&#x60; means call options, while &#x60;false&#x60; is put options</value>
        [JsonProperty("is_call")]
        public bool IsCall { get; set; }

        /// <summary>
        /// Multiplier used in converting from invoicing to settlement currency
        /// </summary>
        /// <value>Multiplier used in converting from invoicing to settlement currency</value>
        [JsonProperty("multiplier")]
        public string Multiplier { get; set; }

        /// <summary>
        /// Underlying
        /// </summary>
        /// <value>Underlying</value>
        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        /// <summary>
        /// Underlying price (quote currency)
        /// </summary>
        /// <value>Underlying price (quote currency)</value>
        [JsonProperty("underlying_price")]
        public string UnderlyingPrice { get; set; }

        /// <summary>
        /// Last trading price
        /// </summary>
        /// <value>Last trading price</value>
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
        /// Maker fee rate, where negative means rebate
        /// </summary>
        /// <value>Maker fee rate, where negative means rebate</value>
        [JsonProperty("maker_fee_rate")]
        public string MakerFeeRate { get; set; }

        /// <summary>
        /// Taker fee rate
        /// </summary>
        /// <value>Taker fee rate</value>
        [JsonProperty("taker_fee_rate")]
        public string TakerFeeRate { get; set; }

        /// <summary>
        /// Minimum order price increment
        /// </summary>
        /// <value>Minimum order price increment</value>
        [JsonProperty("order_price_round")]
        public string OrderPriceRound { get; set; }

        /// <summary>
        /// Minimum mark price increment
        /// </summary>
        /// <value>Minimum mark price increment</value>
        [JsonProperty("mark_price_round")]
        public string MarkPriceRound { get; set; }

        /// <summary>
        /// Minimum order size the contract allowed
        /// </summary>
        /// <value>Minimum order size the contract allowed</value>
        [JsonProperty("order_size_min")]
        public long OrderSizeMin { get; set; }

        /// <summary>
        /// Maximum order size the contract allowed
        /// </summary>
        /// <value>Maximum order size the contract allowed</value>
        [JsonProperty("order_size_max")]
        public long OrderSizeMax { get; set; }

        /// <summary>
        /// deviation between order price and current index price. If price of an order is denoted as order_price, it must meet the following condition:      abs(order_price - mark_price) &lt;&#x3D; mark_price * order_price_deviate
        /// </summary>
        /// <value>deviation between order price and current index price. If price of an order is denoted as order_price, it must meet the following condition:      abs(order_price - mark_price) &lt;&#x3D; mark_price * order_price_deviate</value>
        [JsonProperty("order_price_deviate")]
        public string OrderPriceDeviate { get; set; }

        /// <summary>
        /// Referral fee rate discount
        /// </summary>
        /// <value>Referral fee rate discount</value>
        [JsonProperty("ref_discount_rate")]
        public string RefDiscountRate { get; set; }

        /// <summary>
        /// Referrer commission rate
        /// </summary>
        /// <value>Referrer commission rate</value>
        [JsonProperty("ref_rebate_rate")]
        public string RefRebateRate { get; set; }

        /// <summary>
        /// Current orderbook ID
        /// </summary>
        /// <value>Current orderbook ID</value>
        [JsonProperty("orderbook_id")]
        public long OrderbookId { get; set; }

        /// <summary>
        /// Current trade ID
        /// </summary>
        /// <value>Current trade ID</value>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// Historical accumulated trade size
        /// </summary>
        /// <value>Historical accumulated trade size</value>
        [JsonProperty("trade_size")]
        public long TradeSize { get; set; }

        /// <summary>
        /// Current total long position size
        /// </summary>
        /// <value>Current total long position size</value>
        [JsonProperty("position_size")]
        public long PositionSize { get; set; }

        /// <summary>
        /// Maximum number of open orders
        /// </summary>
        /// <value>Maximum number of open orders</value>
        [JsonProperty("orders_limit")]
        public int OrdersLimit { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsContract {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Tag: ").Append(Tag).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  ExpirationTime: ").Append(ExpirationTime).Append("\n");
            sb.Append("  IsCall: ").Append(IsCall).Append("\n");
            sb.Append("  Multiplier: ").Append(Multiplier).Append("\n");
            sb.Append("  Underlying: ").Append(Underlying).Append("\n");
            sb.Append("  UnderlyingPrice: ").Append(UnderlyingPrice).Append("\n");
            sb.Append("  LastPrice: ").Append(LastPrice).Append("\n");
            sb.Append("  MarkPrice: ").Append(MarkPrice).Append("\n");
            sb.Append("  IndexPrice: ").Append(IndexPrice).Append("\n");
            sb.Append("  MakerFeeRate: ").Append(MakerFeeRate).Append("\n");
            sb.Append("  TakerFeeRate: ").Append(TakerFeeRate).Append("\n");
            sb.Append("  OrderPriceRound: ").Append(OrderPriceRound).Append("\n");
            sb.Append("  MarkPriceRound: ").Append(MarkPriceRound).Append("\n");
            sb.Append("  OrderSizeMin: ").Append(OrderSizeMin).Append("\n");
            sb.Append("  OrderSizeMax: ").Append(OrderSizeMax).Append("\n");
            sb.Append("  OrderPriceDeviate: ").Append(OrderPriceDeviate).Append("\n");
            sb.Append("  RefDiscountRate: ").Append(RefDiscountRate).Append("\n");
            sb.Append("  RefRebateRate: ").Append(RefRebateRate).Append("\n");
            sb.Append("  OrderbookId: ").Append(OrderbookId).Append("\n");
            sb.Append("  TradeId: ").Append(TradeId).Append("\n");
            sb.Append("  TradeSize: ").Append(TradeSize).Append("\n");
            sb.Append("  PositionSize: ").Append(PositionSize).Append("\n");
            sb.Append("  OrdersLimit: ").Append(OrdersLimit).Append("\n");
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
            return this.Equals(input as OptionsContract);
        }

        /// <summary>
        /// Returns true if OptionsContract instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsContract to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsContract input)
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
                    this.Tag == input.Tag ||
                    (this.Tag != null &&
                    this.Tag.Equals(input.Tag))
                ) && 
                (
                    this.CreateTime == input.CreateTime ||
                    this.CreateTime.Equals(input.CreateTime)
                ) && 
                (
                    this.ExpirationTime == input.ExpirationTime ||
                    this.ExpirationTime.Equals(input.ExpirationTime)
                ) && 
                (
                    this.IsCall == input.IsCall ||
                    this.IsCall.Equals(input.IsCall)
                ) && 
                (
                    this.Multiplier == input.Multiplier ||
                    (this.Multiplier != null &&
                    this.Multiplier.Equals(input.Multiplier))
                ) && 
                (
                    this.Underlying == input.Underlying ||
                    (this.Underlying != null &&
                    this.Underlying.Equals(input.Underlying))
                ) && 
                (
                    this.UnderlyingPrice == input.UnderlyingPrice ||
                    (this.UnderlyingPrice != null &&
                    this.UnderlyingPrice.Equals(input.UnderlyingPrice))
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
                    this.MakerFeeRate == input.MakerFeeRate ||
                    (this.MakerFeeRate != null &&
                    this.MakerFeeRate.Equals(input.MakerFeeRate))
                ) && 
                (
                    this.TakerFeeRate == input.TakerFeeRate ||
                    (this.TakerFeeRate != null &&
                    this.TakerFeeRate.Equals(input.TakerFeeRate))
                ) && 
                (
                    this.OrderPriceRound == input.OrderPriceRound ||
                    (this.OrderPriceRound != null &&
                    this.OrderPriceRound.Equals(input.OrderPriceRound))
                ) && 
                (
                    this.MarkPriceRound == input.MarkPriceRound ||
                    (this.MarkPriceRound != null &&
                    this.MarkPriceRound.Equals(input.MarkPriceRound))
                ) && 
                (
                    this.OrderSizeMin == input.OrderSizeMin ||
                    this.OrderSizeMin.Equals(input.OrderSizeMin)
                ) && 
                (
                    this.OrderSizeMax == input.OrderSizeMax ||
                    this.OrderSizeMax.Equals(input.OrderSizeMax)
                ) && 
                (
                    this.OrderPriceDeviate == input.OrderPriceDeviate ||
                    (this.OrderPriceDeviate != null &&
                    this.OrderPriceDeviate.Equals(input.OrderPriceDeviate))
                ) && 
                (
                    this.RefDiscountRate == input.RefDiscountRate ||
                    (this.RefDiscountRate != null &&
                    this.RefDiscountRate.Equals(input.RefDiscountRate))
                ) && 
                (
                    this.RefRebateRate == input.RefRebateRate ||
                    (this.RefRebateRate != null &&
                    this.RefRebateRate.Equals(input.RefRebateRate))
                ) && 
                (
                    this.OrderbookId == input.OrderbookId ||
                    this.OrderbookId.Equals(input.OrderbookId)
                ) && 
                (
                    this.TradeId == input.TradeId ||
                    this.TradeId.Equals(input.TradeId)
                ) && 
                (
                    this.TradeSize == input.TradeSize ||
                    this.TradeSize.Equals(input.TradeSize)
                ) && 
                (
                    this.PositionSize == input.PositionSize ||
                    this.PositionSize.Equals(input.PositionSize)
                ) && 
                (
                    this.OrdersLimit == input.OrdersLimit ||
                    this.OrdersLimit.Equals(input.OrdersLimit)
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
                if (this.Tag != null)
                    hashCode = hashCode * 59 + this.Tag.GetHashCode();
                hashCode = hashCode * 59 + this.CreateTime.GetHashCode();
                hashCode = hashCode * 59 + this.ExpirationTime.GetHashCode();
                hashCode = hashCode * 59 + this.IsCall.GetHashCode();
                if (this.Multiplier != null)
                    hashCode = hashCode * 59 + this.Multiplier.GetHashCode();
                if (this.Underlying != null)
                    hashCode = hashCode * 59 + this.Underlying.GetHashCode();
                if (this.UnderlyingPrice != null)
                    hashCode = hashCode * 59 + this.UnderlyingPrice.GetHashCode();
                if (this.LastPrice != null)
                    hashCode = hashCode * 59 + this.LastPrice.GetHashCode();
                if (this.MarkPrice != null)
                    hashCode = hashCode * 59 + this.MarkPrice.GetHashCode();
                if (this.IndexPrice != null)
                    hashCode = hashCode * 59 + this.IndexPrice.GetHashCode();
                if (this.MakerFeeRate != null)
                    hashCode = hashCode * 59 + this.MakerFeeRate.GetHashCode();
                if (this.TakerFeeRate != null)
                    hashCode = hashCode * 59 + this.TakerFeeRate.GetHashCode();
                if (this.OrderPriceRound != null)
                    hashCode = hashCode * 59 + this.OrderPriceRound.GetHashCode();
                if (this.MarkPriceRound != null)
                    hashCode = hashCode * 59 + this.MarkPriceRound.GetHashCode();
                hashCode = hashCode * 59 + this.OrderSizeMin.GetHashCode();
                hashCode = hashCode * 59 + this.OrderSizeMax.GetHashCode();
                if (this.OrderPriceDeviate != null)
                    hashCode = hashCode * 59 + this.OrderPriceDeviate.GetHashCode();
                if (this.RefDiscountRate != null)
                    hashCode = hashCode * 59 + this.RefDiscountRate.GetHashCode();
                if (this.RefRebateRate != null)
                    hashCode = hashCode * 59 + this.RefRebateRate.GetHashCode();
                hashCode = hashCode * 59 + this.OrderbookId.GetHashCode();
                hashCode = hashCode * 59 + this.TradeId.GetHashCode();
                hashCode = hashCode * 59 + this.TradeSize.GetHashCode();
                hashCode = hashCode * 59 + this.PositionSize.GetHashCode();
                hashCode = hashCode * 59 + this.OrdersLimit.GetHashCode();
                return hashCode;
            }
        }
    }

}
