namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsAccount
    /// </summary>
    
    public class OptionsAccount :  IEquatable<OptionsAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsAccount" /> class.
        /// </summary>
        /// <param name="user">User ID.</param>
        /// <param name="total">Total account balance.</param>
        /// <param name="shortEnabled">If the account is allowed to short.</param>
        /// <param name="unrealisedPnl">Unrealized PNL.</param>
        /// <param name="initMargin">Initial position margin.</param>
        /// <param name="maintMargin">Position maintenance margin.</param>
        /// <param name="orderMargin">Order margin of unfinished orders.</param>
        /// <param name="available">Available balance to transfer out or trade.</param>
        /// <param name="point">POINT amount.</param>
        /// <param name="currency">Settle currency.</param>
        public OptionsAccount(int user = default(int), string total = default(string), bool shortEnabled = default(bool), string unrealisedPnl = default(string), string initMargin = default(string), string maintMargin = default(string), string orderMargin = default(string), string available = default(string), string point = default(string), string currency = default(string))
        {
            this.User = user;
            this.Total = total;
            this.ShortEnabled = shortEnabled;
            this.UnrealisedPnl = unrealisedPnl;
            this.InitMargin = initMargin;
            this.MaintMargin = maintMargin;
            this.OrderMargin = orderMargin;
            this.Available = available;
            this.Point = point;
            this.Currency = currency;
        }

        /// <summary>
        /// User ID
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("user")]
        public int User { get; set; }

        /// <summary>
        /// Total account balance
        /// </summary>
        /// <value>Total account balance</value>
        [JsonProperty("total")]
        public string Total { get; set; }

        /// <summary>
        /// If the account is allowed to short
        /// </summary>
        /// <value>If the account is allowed to short</value>
        [JsonProperty("short_enabled")]
        public bool ShortEnabled { get; set; }

        /// <summary>
        /// Unrealized PNL
        /// </summary>
        /// <value>Unrealized PNL</value>
        [JsonProperty("unrealised_pnl")]
        public string UnrealisedPnl { get; set; }

        /// <summary>
        /// Initial position margin
        /// </summary>
        /// <value>Initial position margin</value>
        [JsonProperty("init_margin")]
        public string InitMargin { get; set; }

        /// <summary>
        /// Position maintenance margin
        /// </summary>
        /// <value>Position maintenance margin</value>
        [JsonProperty("maint_margin")]
        public string MaintMargin { get; set; }

        /// <summary>
        /// Order margin of unfinished orders
        /// </summary>
        /// <value>Order margin of unfinished orders</value>
        [JsonProperty("order_margin")]
        public string OrderMargin { get; set; }

        /// <summary>
        /// Available balance to transfer out or trade
        /// </summary>
        /// <value>Available balance to transfer out or trade</value>
        [JsonProperty("available")]
        public string Available { get; set; }

        /// <summary>
        /// POINT amount
        /// </summary>
        /// <value>POINT amount</value>
        [JsonProperty("point")]
        public string Point { get; set; }

        /// <summary>
        /// Settle currency
        /// </summary>
        /// <value>Settle currency</value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsAccount {\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  Total: ").Append(Total).Append("\n");
            sb.Append("  ShortEnabled: ").Append(ShortEnabled).Append("\n");
            sb.Append("  UnrealisedPnl: ").Append(UnrealisedPnl).Append("\n");
            sb.Append("  InitMargin: ").Append(InitMargin).Append("\n");
            sb.Append("  MaintMargin: ").Append(MaintMargin).Append("\n");
            sb.Append("  OrderMargin: ").Append(OrderMargin).Append("\n");
            sb.Append("  Available: ").Append(Available).Append("\n");
            sb.Append("  Point: ").Append(Point).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
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
            return this.Equals(input as OptionsAccount);
        }

        /// <summary>
        /// Returns true if OptionsAccount instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsAccount to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsAccount input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.User == input.User ||
                    this.User.Equals(input.User)
                ) && 
                (
                    this.Total == input.Total ||
                    (this.Total != null &&
                    this.Total.Equals(input.Total))
                ) && 
                (
                    this.ShortEnabled == input.ShortEnabled ||
                    this.ShortEnabled.Equals(input.ShortEnabled)
                ) && 
                (
                    this.UnrealisedPnl == input.UnrealisedPnl ||
                    (this.UnrealisedPnl != null &&
                    this.UnrealisedPnl.Equals(input.UnrealisedPnl))
                ) && 
                (
                    this.InitMargin == input.InitMargin ||
                    (this.InitMargin != null &&
                    this.InitMargin.Equals(input.InitMargin))
                ) && 
                (
                    this.MaintMargin == input.MaintMargin ||
                    (this.MaintMargin != null &&
                    this.MaintMargin.Equals(input.MaintMargin))
                ) && 
                (
                    this.OrderMargin == input.OrderMargin ||
                    (this.OrderMargin != null &&
                    this.OrderMargin.Equals(input.OrderMargin))
                ) && 
                (
                    this.Available == input.Available ||
                    (this.Available != null &&
                    this.Available.Equals(input.Available))
                ) && 
                (
                    this.Point == input.Point ||
                    (this.Point != null &&
                    this.Point.Equals(input.Point))
                ) && 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
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
                if (this.Total != null)
                    hashCode = hashCode * 59 + this.Total.GetHashCode();
                hashCode = hashCode * 59 + this.ShortEnabled.GetHashCode();
                if (this.UnrealisedPnl != null)
                    hashCode = hashCode * 59 + this.UnrealisedPnl.GetHashCode();
                if (this.InitMargin != null)
                    hashCode = hashCode * 59 + this.InitMargin.GetHashCode();
                if (this.MaintMargin != null)
                    hashCode = hashCode * 59 + this.MaintMargin.GetHashCode();
                if (this.OrderMargin != null)
                    hashCode = hashCode * 59 + this.OrderMargin.GetHashCode();
                if (this.Available != null)
                    hashCode = hashCode * 59 + this.Available.GetHashCode();
                if (this.Point != null)
                    hashCode = hashCode * 59 + this.Point.GetHashCode();
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                return hashCode;
            }
        }
    }

}
