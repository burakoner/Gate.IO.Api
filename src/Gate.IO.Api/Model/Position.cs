namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Futures position details
    /// </summary>
    
    public class Position :  IEquatable<Position>
    {
        /// <summary>
        /// Position mode, including:  - &#x60;single&#x60;: dual mode is not enabled- &#x60;dual_long&#x60;: long position in dual mode- &#x60;dual_short&#x60;: short position in dual mode
        /// </summary>
        /// <value>Position mode, including:  - &#x60;single&#x60;: dual mode is not enabled- &#x60;dual_long&#x60;: long position in dual mode- &#x60;dual_short&#x60;: short position in dual mode</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ModeEnum
        {
            /// <summary>
            /// Enum Single for value: single
            /// </summary>
            [EnumMember(Value = "single")]
            Single = 1,

            /// <summary>
            /// Enum Duallong for value: dual_long
            /// </summary>
            [EnumMember(Value = "dual_long")]
            Duallong = 2,

            /// <summary>
            /// Enum Dualshort for value: dual_short
            /// </summary>
            [EnumMember(Value = "dual_short")]
            Dualshort = 3

        }

        /// <summary>
        /// Position mode, including:  - &#x60;single&#x60;: dual mode is not enabled- &#x60;dual_long&#x60;: long position in dual mode- &#x60;dual_short&#x60;: short position in dual mode
        /// </summary>
        /// <value>Position mode, including:  - &#x60;single&#x60;: dual mode is not enabled- &#x60;dual_long&#x60;: long position in dual mode- &#x60;dual_short&#x60;: short position in dual mode</value>
        [JsonProperty("mode")]
        public ModeEnum? Mode { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Position" /> class.
        /// </summary>
        /// <param name="leverage">Position leverage. 0 means cross margin; positive number means isolated margin.</param>
        /// <param name="riskLimit">Position risk limit.</param>
        /// <param name="margin">Position margin.</param>
        /// <param name="closeOrder">closeOrder.</param>
        /// <param name="mode">Position mode, including:  - &#x60;single&#x60;: dual mode is not enabled- &#x60;dual_long&#x60;: long position in dual mode- &#x60;dual_short&#x60;: short position in dual mode.</param>
        /// <param name="crossLeverageLimit">Cross margin leverage(valid only when &#x60;leverage&#x60; is 0).</param>
        public Position(string leverage = default(string), string riskLimit = default(string), string margin = default(string), PositionCloseOrder closeOrder = default(PositionCloseOrder), ModeEnum? mode = default(ModeEnum?), string crossLeverageLimit = default(string))
        {
            this.Leverage = leverage;
            this.RiskLimit = riskLimit;
            this.Margin = margin;
            this.CloseOrder = closeOrder;
            this.Mode = mode;
            this.CrossLeverageLimit = crossLeverageLimit;
        }

        /// <summary>
        /// User ID
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("user")]
        public long User { get; private set; }

        /// <summary>
        /// Futures contract
        /// </summary>
        /// <value>Futures contract</value>
        [JsonProperty("contract")]
        public string Contract { get; private set; }

        /// <summary>
        /// Position size
        /// </summary>
        /// <value>Position size</value>
        [JsonProperty("size")]
        public long Size { get; private set; }

        /// <summary>
        /// Position leverage. 0 means cross margin; positive number means isolated margin
        /// </summary>
        /// <value>Position leverage. 0 means cross margin; positive number means isolated margin</value>
        [JsonProperty("leverage")]
        public string Leverage { get; set; }

        /// <summary>
        /// Position risk limit
        /// </summary>
        /// <value>Position risk limit</value>
        [JsonProperty("risk_limit")]
        public string RiskLimit { get; set; }

        /// <summary>
        /// Maximum leverage under current risk limit
        /// </summary>
        /// <value>Maximum leverage under current risk limit</value>
        [JsonProperty("leverage_max")]
        public string LeverageMax { get; private set; }

        /// <summary>
        /// Maintenance rate under current risk limit
        /// </summary>
        /// <value>Maintenance rate under current risk limit</value>
        [JsonProperty("maintenance_rate")]
        public string MaintenanceRate { get; private set; }

        /// <summary>
        /// Position value calculated in settlement currency
        /// </summary>
        /// <value>Position value calculated in settlement currency</value>
        [JsonProperty("value")]
        public string Value { get; private set; }

        /// <summary>
        /// Position margin
        /// </summary>
        /// <value>Position margin</value>
        [JsonProperty("margin")]
        public string Margin { get; set; }

        /// <summary>
        /// Entry price
        /// </summary>
        /// <value>Entry price</value>
        [JsonProperty("entry_price")]
        public string EntryPrice { get; private set; }

        /// <summary>
        /// Liquidation price
        /// </summary>
        /// <value>Liquidation price</value>
        [JsonProperty("liq_price")]
        public string LiqPrice { get; private set; }

        /// <summary>
        /// Current mark price
        /// </summary>
        /// <value>Current mark price</value>
        [JsonProperty("mark_price")]
        public string MarkPrice { get; private set; }

        /// <summary>
        /// The initial margin occupied by the position, applicable to the portfolio margin account
        /// </summary>
        /// <value>The initial margin occupied by the position, applicable to the portfolio margin account</value>
        [JsonProperty("initial_margin")]
        public string InitialMargin { get; private set; }

        /// <summary>
        /// Maintenance margin required for the position, applicable to portfolio margin account
        /// </summary>
        /// <value>Maintenance margin required for the position, applicable to portfolio margin account</value>
        [JsonProperty("maintenance_margin")]
        public string MaintenanceMargin { get; private set; }

        /// <summary>
        /// Unrealized PNL
        /// </summary>
        /// <value>Unrealized PNL</value>
        [JsonProperty("unrealised_pnl")]
        public string UnrealisedPnl { get; private set; }

        /// <summary>
        /// Realized PNL
        /// </summary>
        /// <value>Realized PNL</value>
        [JsonProperty("realised_pnl")]
        public string RealisedPnl { get; private set; }

        /// <summary>
        /// History realized PNL
        /// </summary>
        /// <value>History realized PNL</value>
        [JsonProperty("history_pnl")]
        public string HistoryPnl { get; private set; }

        /// <summary>
        /// PNL of last position close
        /// </summary>
        /// <value>PNL of last position close</value>
        [JsonProperty("last_close_pnl")]
        public string LastClosePnl { get; private set; }

        /// <summary>
        /// Realized POINT PNL
        /// </summary>
        /// <value>Realized POINT PNL</value>
        [JsonProperty("realised_point")]
        public string RealisedPoint { get; private set; }

        /// <summary>
        /// History realized POINT PNL
        /// </summary>
        /// <value>History realized POINT PNL</value>
        [JsonProperty("history_point")]
        public string HistoryPoint { get; private set; }

        /// <summary>
        /// ADL ranking, ranging from 1 to 5
        /// </summary>
        /// <value>ADL ranking, ranging from 1 to 5</value>
        [JsonProperty("adl_ranking")]
        public int AdlRanking { get; private set; }

        /// <summary>
        /// Current open orders
        /// </summary>
        /// <value>Current open orders</value>
        [JsonProperty("pending_orders")]
        public int PendingOrders { get; private set; }

        /// <summary>
        /// Gets or Sets CloseOrder
        /// </summary>
        [JsonProperty("close_order")]
        public PositionCloseOrder CloseOrder { get; set; }

        /// <summary>
        /// Cross margin leverage(valid only when &#x60;leverage&#x60; is 0)
        /// </summary>
        /// <value>Cross margin leverage(valid only when &#x60;leverage&#x60; is 0)</value>
        [JsonProperty("cross_leverage_limit")]
        public string CrossLeverageLimit { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Position {\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Leverage: ").Append(Leverage).Append("\n");
            sb.Append("  RiskLimit: ").Append(RiskLimit).Append("\n");
            sb.Append("  LeverageMax: ").Append(LeverageMax).Append("\n");
            sb.Append("  MaintenanceRate: ").Append(MaintenanceRate).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  Margin: ").Append(Margin).Append("\n");
            sb.Append("  EntryPrice: ").Append(EntryPrice).Append("\n");
            sb.Append("  LiqPrice: ").Append(LiqPrice).Append("\n");
            sb.Append("  MarkPrice: ").Append(MarkPrice).Append("\n");
            sb.Append("  InitialMargin: ").Append(InitialMargin).Append("\n");
            sb.Append("  MaintenanceMargin: ").Append(MaintenanceMargin).Append("\n");
            sb.Append("  UnrealisedPnl: ").Append(UnrealisedPnl).Append("\n");
            sb.Append("  RealisedPnl: ").Append(RealisedPnl).Append("\n");
            sb.Append("  HistoryPnl: ").Append(HistoryPnl).Append("\n");
            sb.Append("  LastClosePnl: ").Append(LastClosePnl).Append("\n");
            sb.Append("  RealisedPoint: ").Append(RealisedPoint).Append("\n");
            sb.Append("  HistoryPoint: ").Append(HistoryPoint).Append("\n");
            sb.Append("  AdlRanking: ").Append(AdlRanking).Append("\n");
            sb.Append("  PendingOrders: ").Append(PendingOrders).Append("\n");
            sb.Append("  CloseOrder: ").Append(CloseOrder).Append("\n");
            sb.Append("  Mode: ").Append(Mode).Append("\n");
            sb.Append("  CrossLeverageLimit: ").Append(CrossLeverageLimit).Append("\n");
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
            return this.Equals(input as Position);
        }

        /// <summary>
        /// Returns true if Position instances are equal
        /// </summary>
        /// <param name="input">Instance of Position to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Position input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.User == input.User ||
                    this.User.Equals(input.User)
                ) && 
                (
                    this.Contract == input.Contract ||
                    (this.Contract != null &&
                    this.Contract.Equals(input.Contract))
                ) && 
                (
                    this.Size == input.Size ||
                    this.Size.Equals(input.Size)
                ) && 
                (
                    this.Leverage == input.Leverage ||
                    (this.Leverage != null &&
                    this.Leverage.Equals(input.Leverage))
                ) && 
                (
                    this.RiskLimit == input.RiskLimit ||
                    (this.RiskLimit != null &&
                    this.RiskLimit.Equals(input.RiskLimit))
                ) && 
                (
                    this.LeverageMax == input.LeverageMax ||
                    (this.LeverageMax != null &&
                    this.LeverageMax.Equals(input.LeverageMax))
                ) && 
                (
                    this.MaintenanceRate == input.MaintenanceRate ||
                    (this.MaintenanceRate != null &&
                    this.MaintenanceRate.Equals(input.MaintenanceRate))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.Margin == input.Margin ||
                    (this.Margin != null &&
                    this.Margin.Equals(input.Margin))
                ) && 
                (
                    this.EntryPrice == input.EntryPrice ||
                    (this.EntryPrice != null &&
                    this.EntryPrice.Equals(input.EntryPrice))
                ) && 
                (
                    this.LiqPrice == input.LiqPrice ||
                    (this.LiqPrice != null &&
                    this.LiqPrice.Equals(input.LiqPrice))
                ) && 
                (
                    this.MarkPrice == input.MarkPrice ||
                    (this.MarkPrice != null &&
                    this.MarkPrice.Equals(input.MarkPrice))
                ) && 
                (
                    this.InitialMargin == input.InitialMargin ||
                    (this.InitialMargin != null &&
                    this.InitialMargin.Equals(input.InitialMargin))
                ) && 
                (
                    this.MaintenanceMargin == input.MaintenanceMargin ||
                    (this.MaintenanceMargin != null &&
                    this.MaintenanceMargin.Equals(input.MaintenanceMargin))
                ) && 
                (
                    this.UnrealisedPnl == input.UnrealisedPnl ||
                    (this.UnrealisedPnl != null &&
                    this.UnrealisedPnl.Equals(input.UnrealisedPnl))
                ) && 
                (
                    this.RealisedPnl == input.RealisedPnl ||
                    (this.RealisedPnl != null &&
                    this.RealisedPnl.Equals(input.RealisedPnl))
                ) && 
                (
                    this.HistoryPnl == input.HistoryPnl ||
                    (this.HistoryPnl != null &&
                    this.HistoryPnl.Equals(input.HistoryPnl))
                ) && 
                (
                    this.LastClosePnl == input.LastClosePnl ||
                    (this.LastClosePnl != null &&
                    this.LastClosePnl.Equals(input.LastClosePnl))
                ) && 
                (
                    this.RealisedPoint == input.RealisedPoint ||
                    (this.RealisedPoint != null &&
                    this.RealisedPoint.Equals(input.RealisedPoint))
                ) && 
                (
                    this.HistoryPoint == input.HistoryPoint ||
                    (this.HistoryPoint != null &&
                    this.HistoryPoint.Equals(input.HistoryPoint))
                ) && 
                (
                    this.AdlRanking == input.AdlRanking ||
                    this.AdlRanking.Equals(input.AdlRanking)
                ) && 
                (
                    this.PendingOrders == input.PendingOrders ||
                    this.PendingOrders.Equals(input.PendingOrders)
                ) && 
                (
                    this.CloseOrder == input.CloseOrder ||
                    (this.CloseOrder != null &&
                    this.CloseOrder.Equals(input.CloseOrder))
                ) && 
                (
                    this.Mode == input.Mode ||
                    this.Mode.Equals(input.Mode)
                ) && 
                (
                    this.CrossLeverageLimit == input.CrossLeverageLimit ||
                    (this.CrossLeverageLimit != null &&
                    this.CrossLeverageLimit.Equals(input.CrossLeverageLimit))
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
                hashCode = hashCode * 59 + this.User.GetHashCode();
                if (this.Contract != null)
                    hashCode = hashCode * 59 + this.Contract.GetHashCode();
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Leverage != null)
                    hashCode = hashCode * 59 + this.Leverage.GetHashCode();
                if (this.RiskLimit != null)
                    hashCode = hashCode * 59 + this.RiskLimit.GetHashCode();
                if (this.LeverageMax != null)
                    hashCode = hashCode * 59 + this.LeverageMax.GetHashCode();
                if (this.MaintenanceRate != null)
                    hashCode = hashCode * 59 + this.MaintenanceRate.GetHashCode();
                if (this.Value != null)
                    hashCode = hashCode * 59 + this.Value.GetHashCode();
                if (this.Margin != null)
                    hashCode = hashCode * 59 + this.Margin.GetHashCode();
                if (this.EntryPrice != null)
                    hashCode = hashCode * 59 + this.EntryPrice.GetHashCode();
                if (this.LiqPrice != null)
                    hashCode = hashCode * 59 + this.LiqPrice.GetHashCode();
                if (this.MarkPrice != null)
                    hashCode = hashCode * 59 + this.MarkPrice.GetHashCode();
                if (this.InitialMargin != null)
                    hashCode = hashCode * 59 + this.InitialMargin.GetHashCode();
                if (this.MaintenanceMargin != null)
                    hashCode = hashCode * 59 + this.MaintenanceMargin.GetHashCode();
                if (this.UnrealisedPnl != null)
                    hashCode = hashCode * 59 + this.UnrealisedPnl.GetHashCode();
                if (this.RealisedPnl != null)
                    hashCode = hashCode * 59 + this.RealisedPnl.GetHashCode();
                if (this.HistoryPnl != null)
                    hashCode = hashCode * 59 + this.HistoryPnl.GetHashCode();
                if (this.LastClosePnl != null)
                    hashCode = hashCode * 59 + this.LastClosePnl.GetHashCode();
                if (this.RealisedPoint != null)
                    hashCode = hashCode * 59 + this.RealisedPoint.GetHashCode();
                if (this.HistoryPoint != null)
                    hashCode = hashCode * 59 + this.HistoryPoint.GetHashCode();
                hashCode = hashCode * 59 + this.AdlRanking.GetHashCode();
                hashCode = hashCode * 59 + this.PendingOrders.GetHashCode();
                if (this.CloseOrder != null)
                    hashCode = hashCode * 59 + this.CloseOrder.GetHashCode();
                hashCode = hashCode * 59 + this.Mode.GetHashCode();
                if (this.CrossLeverageLimit != null)
                    hashCode = hashCode * 59 + this.CrossLeverageLimit.GetHashCode();
                return hashCode;
            }
        }
    }

}
