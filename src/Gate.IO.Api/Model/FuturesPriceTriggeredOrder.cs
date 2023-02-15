namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Futures order details
    /// </summary>
    
    public class FuturesPriceTriggeredOrder :  IEquatable<FuturesPriceTriggeredOrder>
    {
        /// <summary>
        /// Auto order status  - &#x60;open&#x60;: order is active - &#x60;finished&#x60;: order is finished - &#x60;inactive&#x60;: order is not active, only for close-long-order or close-short-order - &#x60;invalid&#x60;: order is invalid, only for close-long-order or close-short-order
        /// </summary>
        /// <value>Auto order status  - &#x60;open&#x60;: order is active - &#x60;finished&#x60;: order is finished - &#x60;inactive&#x60;: order is not active, only for close-long-order or close-short-order - &#x60;invalid&#x60;: order is invalid, only for close-long-order or close-short-order</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Open for value: open
            /// </summary>
            [EnumMember(Value = "open")]
            Open = 1,

            /// <summary>
            /// Enum Finished for value: finished
            /// </summary>
            [EnumMember(Value = "finished")]
            Finished = 2,

            /// <summary>
            /// Enum Inactive for value: inactive
            /// </summary>
            [EnumMember(Value = "inactive")]
            Inactive = 3,

            /// <summary>
            /// Enum Invalid for value: invalid
            /// </summary>
            [EnumMember(Value = "invalid")]
            Invalid = 4

        }

        /// <summary>
        /// Auto order status  - &#x60;open&#x60;: order is active - &#x60;finished&#x60;: order is finished - &#x60;inactive&#x60;: order is not active, only for close-long-order or close-short-order - &#x60;invalid&#x60;: order is invalid, only for close-long-order or close-short-order
        /// </summary>
        /// <value>Auto order status  - &#x60;open&#x60;: order is active - &#x60;finished&#x60;: order is finished - &#x60;inactive&#x60;: order is not active, only for close-long-order or close-short-order - &#x60;invalid&#x60;: order is invalid, only for close-long-order or close-short-order</value>
        [JsonProperty("status")]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// How order is finished
        /// </summary>
        /// <value>How order is finished</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum FinishAsEnum
        {
            /// <summary>
            /// Enum Cancelled for value: cancelled
            /// </summary>
            [EnumMember(Value = "cancelled")]
            Cancelled = 1,

            /// <summary>
            /// Enum Succeeded for value: succeeded
            /// </summary>
            [EnumMember(Value = "succeeded")]
            Succeeded = 2,

            /// <summary>
            /// Enum Failed for value: failed
            /// </summary>
            [EnumMember(Value = "failed")]
            Failed = 3,

            /// <summary>
            /// Enum Expired for value: expired
            /// </summary>
            [EnumMember(Value = "expired")]
            Expired = 4

        }

        /// <summary>
        /// How order is finished
        /// </summary>
        /// <value>How order is finished</value>
        [JsonProperty("finish_as")]
        public FinishAsEnum? FinishAs { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesPriceTriggeredOrder" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected FuturesPriceTriggeredOrder() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesPriceTriggeredOrder" /> class.
        /// </summary>
        /// <param name="initial">initial (required).</param>
        /// <param name="trigger">trigger (required).</param>
        /// <param name="orderType">Take-profit/stop-loss types, which include:  - &#x60;close-long-order&#x60;: order take-profit/stop-loss, close long position - &#x60;close-short-order&#x60;: order take-profit/stop-loss, close short position - &#x60;close-long-position&#x60;: position take-profit/stop-loss, close long position - &#x60;close-short-position&#x60;: position take-profit/stop-loss, close short position - &#x60;plan-close-long-position&#x60;: position planned take-profit/stop-loss, close long position - &#x60;plan-close-short-position&#x60;: position planned take-profit/stop-loss, close short position  The order take-profit/stop-loss can not be passed by request. These two types are read only..</param>
        public FuturesPriceTriggeredOrder(FuturesInitialOrder initial = default(FuturesInitialOrder), FuturesPriceTrigger trigger = default(FuturesPriceTrigger), string orderType = default(string))
        {
            // to ensure "initial" is required (not null)
            this.Initial = initial ?? throw new ArgumentNullException("initial", "initial is a required property for FuturesPriceTriggeredOrder and cannot be null");
            // to ensure "trigger" is required (not null)
            this.Trigger = trigger ?? throw new ArgumentNullException("trigger", "trigger is a required property for FuturesPriceTriggeredOrder and cannot be null");
            this.OrderType = orderType;
        }

        /// <summary>
        /// Gets or Sets Initial
        /// </summary>
        [JsonProperty("initial")]
        public FuturesInitialOrder Initial { get; set; }

        /// <summary>
        /// Gets or Sets Trigger
        /// </summary>
        [JsonProperty("trigger")]
        public FuturesPriceTrigger Trigger { get; set; }

        /// <summary>
        /// Auto order ID
        /// </summary>
        /// <value>Auto order ID</value>
        [JsonProperty("id")]
        public long Id { get; private set; }

        /// <summary>
        /// User ID
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("user")]
        public int User { get; private set; }

        /// <summary>
        /// Creation time
        /// </summary>
        /// <value>Creation time</value>
        [JsonProperty("create_time")]
        public double CreateTime { get; private set; }

        /// <summary>
        /// Finished time
        /// </summary>
        /// <value>Finished time</value>
        [JsonProperty("finish_time")]
        public double FinishTime { get; private set; }

        /// <summary>
        /// ID of the newly created order on condition triggered
        /// </summary>
        /// <value>ID of the newly created order on condition triggered</value>
        [JsonProperty("trade_id")]
        public long TradeId { get; private set; }

        /// <summary>
        /// Additional remarks on how the order was finished
        /// </summary>
        /// <value>Additional remarks on how the order was finished</value>
        [JsonProperty("reason")]
        public string Reason { get; private set; }

        /// <summary>
        /// Take-profit/stop-loss types, which include:  - &#x60;close-long-order&#x60;: order take-profit/stop-loss, close long position - &#x60;close-short-order&#x60;: order take-profit/stop-loss, close short position - &#x60;close-long-position&#x60;: position take-profit/stop-loss, close long position - &#x60;close-short-position&#x60;: position take-profit/stop-loss, close short position - &#x60;plan-close-long-position&#x60;: position planned take-profit/stop-loss, close long position - &#x60;plan-close-short-position&#x60;: position planned take-profit/stop-loss, close short position  The order take-profit/stop-loss can not be passed by request. These two types are read only.
        /// </summary>
        /// <value>Take-profit/stop-loss types, which include:  - &#x60;close-long-order&#x60;: order take-profit/stop-loss, close long position - &#x60;close-short-order&#x60;: order take-profit/stop-loss, close short position - &#x60;close-long-position&#x60;: position take-profit/stop-loss, close long position - &#x60;close-short-position&#x60;: position take-profit/stop-loss, close short position - &#x60;plan-close-long-position&#x60;: position planned take-profit/stop-loss, close long position - &#x60;plan-close-short-position&#x60;: position planned take-profit/stop-loss, close short position  The order take-profit/stop-loss can not be passed by request. These two types are read only.</value>
        [JsonProperty("order_type")]
        public string OrderType { get; set; }

        /// <summary>
        /// Corresponding order ID of order take-profit/stop-loss.
        /// </summary>
        /// <value>Corresponding order ID of order take-profit/stop-loss.</value>
        [JsonProperty("me_order_id")]
        public string MeOrderId { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesPriceTriggeredOrder {\n");
            sb.Append("  Initial: ").Append(Initial).Append("\n");
            sb.Append("  Trigger: ").Append(Trigger).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  FinishTime: ").Append(FinishTime).Append("\n");
            sb.Append("  TradeId: ").Append(TradeId).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  FinishAs: ").Append(FinishAs).Append("\n");
            sb.Append("  Reason: ").Append(Reason).Append("\n");
            sb.Append("  OrderType: ").Append(OrderType).Append("\n");
            sb.Append("  MeOrderId: ").Append(MeOrderId).Append("\n");
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
            return this.Equals(input as FuturesPriceTriggeredOrder);
        }

        /// <summary>
        /// Returns true if FuturesPriceTriggeredOrder instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesPriceTriggeredOrder to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesPriceTriggeredOrder input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Initial == input.Initial ||
                    (this.Initial != null &&
                    this.Initial.Equals(input.Initial))
                ) && 
                (
                    this.Trigger == input.Trigger ||
                    (this.Trigger != null &&
                    this.Trigger.Equals(input.Trigger))
                ) && 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.User == input.User ||
                    this.User.Equals(input.User)
                ) && 
                (
                    this.CreateTime == input.CreateTime ||
                    this.CreateTime.Equals(input.CreateTime)
                ) && 
                (
                    this.FinishTime == input.FinishTime ||
                    this.FinishTime.Equals(input.FinishTime)
                ) && 
                (
                    this.TradeId == input.TradeId ||
                    this.TradeId.Equals(input.TradeId)
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.FinishAs == input.FinishAs ||
                    this.FinishAs.Equals(input.FinishAs)
                ) && 
                (
                    this.Reason == input.Reason ||
                    (this.Reason != null &&
                    this.Reason.Equals(input.Reason))
                ) && 
                (
                    this.OrderType == input.OrderType ||
                    (this.OrderType != null &&
                    this.OrderType.Equals(input.OrderType))
                ) && 
                (
                    this.MeOrderId == input.MeOrderId ||
                    (this.MeOrderId != null &&
                    this.MeOrderId.Equals(input.MeOrderId))
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
                if (this.Initial != null)
                    hashCode = hashCode * 59 + this.Initial.GetHashCode();
                if (this.Trigger != null)
                    hashCode = hashCode * 59 + this.Trigger.GetHashCode();
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.User.GetHashCode();
                hashCode = hashCode * 59 + this.CreateTime.GetHashCode();
                hashCode = hashCode * 59 + this.FinishTime.GetHashCode();
                hashCode = hashCode * 59 + this.TradeId.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                hashCode = hashCode * 59 + this.FinishAs.GetHashCode();
                if (this.Reason != null)
                    hashCode = hashCode * 59 + this.Reason.GetHashCode();
                if (this.OrderType != null)
                    hashCode = hashCode * 59 + this.OrderType.GetHashCode();
                if (this.MeOrderId != null)
                    hashCode = hashCode * 59 + this.MeOrderId.GetHashCode();
                return hashCode;
            }
        }
    }

}
