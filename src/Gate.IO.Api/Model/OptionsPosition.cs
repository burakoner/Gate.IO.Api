namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Options position information
    /// </summary>
    
    public class OptionsPosition :  IEquatable<OptionsPosition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPosition" /> class.
        /// </summary>
        /// <param name="closeOrder">closeOrder.</param>
        public OptionsPosition(OptionsPositionCloseOrder closeOrder = default(OptionsPositionCloseOrder))
        {
            this.CloseOrder = closeOrder;
        }

        /// <summary>
        /// User ID
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("user")]
        public int User { get; private set; }

        /// <summary>
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("contract")]
        public string Contract { get; private set; }

        /// <summary>
        /// Position size (contract size)
        /// </summary>
        /// <value>Position size (contract size)</value>
        [JsonProperty("size")]
        public long Size { get; private set; }

        /// <summary>
        /// Entry size (quote currency)
        /// </summary>
        /// <value>Entry size (quote currency)</value>
        [JsonProperty("entry_price")]
        public string EntryPrice { get; private set; }

        /// <summary>
        /// Current mark price (quote currency)
        /// </summary>
        /// <value>Current mark price (quote currency)</value>
        [JsonProperty("mark_price")]
        public string MarkPrice { get; private set; }

        /// <summary>
        /// Realized PNL
        /// </summary>
        /// <value>Realized PNL</value>
        [JsonProperty("realised_pnl")]
        public string RealisedPnl { get; private set; }

        /// <summary>
        /// Unrealized PNL
        /// </summary>
        /// <value>Unrealized PNL</value>
        [JsonProperty("unrealised_pnl")]
        public string UnrealisedPnl { get; private set; }

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
        public OptionsPositionCloseOrder CloseOrder { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsPosition {\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  EntryPrice: ").Append(EntryPrice).Append("\n");
            sb.Append("  MarkPrice: ").Append(MarkPrice).Append("\n");
            sb.Append("  RealisedPnl: ").Append(RealisedPnl).Append("\n");
            sb.Append("  UnrealisedPnl: ").Append(UnrealisedPnl).Append("\n");
            sb.Append("  PendingOrders: ").Append(PendingOrders).Append("\n");
            sb.Append("  CloseOrder: ").Append(CloseOrder).Append("\n");
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
            return this.Equals(input as OptionsPosition);
        }

        /// <summary>
        /// Returns true if OptionsPosition instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsPosition to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsPosition input)
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
                    this.EntryPrice == input.EntryPrice ||
                    (this.EntryPrice != null &&
                    this.EntryPrice.Equals(input.EntryPrice))
                ) && 
                (
                    this.MarkPrice == input.MarkPrice ||
                    (this.MarkPrice != null &&
                    this.MarkPrice.Equals(input.MarkPrice))
                ) && 
                (
                    this.RealisedPnl == input.RealisedPnl ||
                    (this.RealisedPnl != null &&
                    this.RealisedPnl.Equals(input.RealisedPnl))
                ) && 
                (
                    this.UnrealisedPnl == input.UnrealisedPnl ||
                    (this.UnrealisedPnl != null &&
                    this.UnrealisedPnl.Equals(input.UnrealisedPnl))
                ) && 
                (
                    this.PendingOrders == input.PendingOrders ||
                    this.PendingOrders.Equals(input.PendingOrders)
                ) && 
                (
                    this.CloseOrder == input.CloseOrder ||
                    (this.CloseOrder != null &&
                    this.CloseOrder.Equals(input.CloseOrder))
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
                if (this.EntryPrice != null)
                    hashCode = hashCode * 59 + this.EntryPrice.GetHashCode();
                if (this.MarkPrice != null)
                    hashCode = hashCode * 59 + this.MarkPrice.GetHashCode();
                if (this.RealisedPnl != null)
                    hashCode = hashCode * 59 + this.RealisedPnl.GetHashCode();
                if (this.UnrealisedPnl != null)
                    hashCode = hashCode * 59 + this.UnrealisedPnl.GetHashCode();
                hashCode = hashCode * 59 + this.PendingOrders.GetHashCode();
                if (this.CloseOrder != null)
                    hashCode = hashCode * 59 + this.CloseOrder.GetHashCode();
                return hashCode;
            }
        }
    }

}
