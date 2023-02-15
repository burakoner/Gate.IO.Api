namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// Batch order details
    /// </summary>
    public class BatchOrder :  IEquatable<BatchOrder>
    {
        /// <summary>
        /// Order status  - &#x60;open&#x60;: to be filled - &#x60;closed&#x60;: filled - &#x60;cancelled&#x60;: cancelled
        /// </summary>
        /// <value>Order status  - &#x60;open&#x60;: to be filled - &#x60;closed&#x60;: filled - &#x60;cancelled&#x60;: cancelled</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Open for value: open
            /// </summary>
            [EnumMember(Value = "open")]
            Open = 1,

            /// <summary>
            /// Enum Closed for value: closed
            /// </summary>
            [EnumMember(Value = "closed")]
            Closed = 2,

            /// <summary>
            /// Enum Cancelled for value: cancelled
            /// </summary>
            [EnumMember(Value = "cancelled")]
            Cancelled = 3

        }

        /// <summary>
        /// Order status  - &#x60;open&#x60;: to be filled - &#x60;closed&#x60;: filled - &#x60;cancelled&#x60;: cancelled
        /// </summary>
        /// <value>Order status  - &#x60;open&#x60;: to be filled - &#x60;closed&#x60;: filled - &#x60;cancelled&#x60;: cancelled</value>
        [JsonProperty("status")]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Order type. limit - limit order
        /// </summary>
        /// <value>Order type. limit - limit order</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum Limit for value: limit
            /// </summary>
            [EnumMember(Value = "limit")]
            Limit = 1

        }

        /// <summary>
        /// Order type. limit - limit order
        /// </summary>
        /// <value>Order type. limit - limit order</value>
        [JsonProperty("type")]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// Account type. spot - use spot account; margin - use margin account; cross_margin - use cross margin account
        /// </summary>
        /// <value>Account type. spot - use spot account; margin - use margin account; cross_margin - use cross margin account</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AccountEnum
        {
            /// <summary>
            /// Enum Spot for value: spot
            /// </summary>
            [EnumMember(Value = "spot")]
            Spot = 1,

            /// <summary>
            /// Enum Margin for value: margin
            /// </summary>
            [EnumMember(Value = "margin")]
            Margin = 2,

            /// <summary>
            /// Enum Crossmargin for value: cross_margin
            /// </summary>
            [EnumMember(Value = "cross_margin")]
            Crossmargin = 3

        }

        /// <summary>
        /// Account type. spot - use spot account; margin - use margin account; cross_margin - use cross margin account
        /// </summary>
        /// <value>Account type. spot - use spot account; margin - use margin account; cross_margin - use cross margin account</value>
        [JsonProperty("account")]
        public AccountEnum? Account { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        /// <value>Order side</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SideEnum
        {
            /// <summary>
            /// Enum Buy for value: buy
            /// </summary>
            [EnumMember(Value = "buy")]
            Buy = 1,

            /// <summary>
            /// Enum Sell for value: sell
            /// </summary>
            [EnumMember(Value = "sell")]
            Sell = 2

        }

        /// <summary>
        /// Order side
        /// </summary>
        /// <value>Order side</value>
        [JsonProperty("side")]
        public SideEnum? Side { get; set; }
        /// <summary>
        /// Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none
        /// </summary>
        /// <value>Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TimeInForceEnum
        {
            /// <summary>
            /// Enum Gtc for value: gtc
            /// </summary>
            [EnumMember(Value = "gtc")]
            Gtc = 1,

            /// <summary>
            /// Enum Ioc for value: ioc
            /// </summary>
            [EnumMember(Value = "ioc")]
            Ioc = 2,

            /// <summary>
            /// Enum Poc for value: poc
            /// </summary>
            [EnumMember(Value = "poc")]
            Poc = 3,

            /// <summary>
            /// Enum Fok for value: fok
            /// </summary>
            [EnumMember(Value = "fok")]
            Fok = 4

        }

        /// <summary>
        /// Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none
        /// </summary>
        /// <value>Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none</value>
        [JsonProperty("time_in_force")]
        public TimeInForceEnum? TimeInForce { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchOrder" /> class.
        /// </summary>
        /// <param name="text">User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) .</param>
        /// <param name="succeeded">Whether the batch of orders succeeded.</param>
        /// <param name="label">Error label, if any, otherwise an empty string.</param>
        /// <param name="message">Detailed error message, if any, otherwise an empty string.</param>
        /// <param name="currencyPair">Currency pair.</param>
        /// <param name="type">Order type. limit - limit order (default to TypeEnum.Limit).</param>
        /// <param name="account">Account type. spot - use spot account; margin - use margin account; cross_margin - use cross margin account (default to AccountEnum.Spot).</param>
        /// <param name="side">Order side.</param>
        /// <param name="amount">Trade amount.</param>
        /// <param name="price">Order price.</param>
        /// <param name="timeInForce">Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none (default to TimeInForceEnum.Gtc).</param>
        /// <param name="iceberg">Amount to display for the iceberg order. Null or 0 for normal orders. Set to -1 to hide the order completely.</param>
        /// <param name="autoBorrow">Used in margin or cross margin trading to allow automatic loan of insufficient amount if balance is not enough..</param>
        /// <param name="autoRepay">Enable or disable automatic repayment for automatic borrow loan generated by cross margin order. Default is disabled. Note that:  1. This field is only effective for cross margin orders. Margin account does not support setting auto repayment for orders. 2. &#x60;auto_borrow&#x60; and &#x60;auto_repay&#x60; cannot be both set to true in one order..</param>
        public BatchOrder(string text = default(string), bool succeeded = default(bool), string label = default(string), string message = default(string), string currencyPair = default(string), TypeEnum? type = TypeEnum.Limit, AccountEnum? account = AccountEnum.Spot, SideEnum? side = default(SideEnum?), string amount = default(string), string price = default(string), TimeInForceEnum? timeInForce = TimeInForceEnum.Gtc, string iceberg = default(string), bool autoBorrow = default(bool), bool autoRepay = default(bool))
        {
            this.Text = text;
            this.Succeeded = succeeded;
            this.Label = label;
            this.Message = message;
            this.CurrencyPair = currencyPair;
            this.Type = type;
            this.Account = account;
            this.Side = side;
            this.Amount = amount;
            this.Price = price;
            this.TimeInForce = timeInForce;
            this.Iceberg = iceberg;
            this.AutoBorrow = autoBorrow;
            this.AutoRepay = autoRepay;
        }

        /// <summary>
        /// User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) 
        /// </summary>
        /// <value>User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) </value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Whether the batch of orders succeeded
        /// </summary>
        /// <value>Whether the batch of orders succeeded</value>
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        /// <summary>
        /// Error label, if any, otherwise an empty string
        /// </summary>
        /// <value>Error label, if any, otherwise an empty string</value>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Detailed error message, if any, otherwise an empty string
        /// </summary>
        /// <value>Detailed error message, if any, otherwise an empty string</value>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        /// <value>Order ID</value>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// Creation time of order
        /// </summary>
        /// <value>Creation time of order</value>
        [JsonProperty("create_time")]
        public string CreateTime { get; private set; }

        /// <summary>
        /// Last modification time of order
        /// </summary>
        /// <value>Last modification time of order</value>
        [JsonProperty("update_time")]
        public string UpdateTime { get; private set; }

        /// <summary>
        /// Creation time of order (in milliseconds)
        /// </summary>
        /// <value>Creation time of order (in milliseconds)</value>
        [JsonProperty("create_time_ms")]
        public long CreateTimeMs { get; private set; }

        /// <summary>
        /// Last modification time of order (in milliseconds)
        /// </summary>
        /// <value>Last modification time of order (in milliseconds)</value>
        [JsonProperty("update_time_ms")]
        public long UpdateTimeMs { get; private set; }

        /// <summary>
        /// Currency pair
        /// </summary>
        /// <value>Currency pair</value>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }

        /// <summary>
        /// Trade amount
        /// </summary>
        /// <value>Trade amount</value>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Order price
        /// </summary>
        /// <value>Order price</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Amount to display for the iceberg order. Null or 0 for normal orders. Set to -1 to hide the order completely
        /// </summary>
        /// <value>Amount to display for the iceberg order. Null or 0 for normal orders. Set to -1 to hide the order completely</value>
        [JsonProperty("iceberg")]
        public string Iceberg { get; set; }

        /// <summary>
        /// Used in margin or cross margin trading to allow automatic loan of insufficient amount if balance is not enough.
        /// </summary>
        /// <value>Used in margin or cross margin trading to allow automatic loan of insufficient amount if balance is not enough.</value>
        [JsonProperty("auto_borrow")]
        public bool AutoBorrow { get; set; }

        /// <summary>
        /// Enable or disable automatic repayment for automatic borrow loan generated by cross margin order. Default is disabled. Note that:  1. This field is only effective for cross margin orders. Margin account does not support setting auto repayment for orders. 2. &#x60;auto_borrow&#x60; and &#x60;auto_repay&#x60; cannot be both set to true in one order.
        /// </summary>
        /// <value>Enable or disable automatic repayment for automatic borrow loan generated by cross margin order. Default is disabled. Note that:  1. This field is only effective for cross margin orders. Margin account does not support setting auto repayment for orders. 2. &#x60;auto_borrow&#x60; and &#x60;auto_repay&#x60; cannot be both set to true in one order.</value>
        [JsonProperty("auto_repay")]
        public bool AutoRepay { get; set; }

        /// <summary>
        /// Amount left to fill
        /// </summary>
        /// <value>Amount left to fill</value>
        [JsonProperty("left")]
        public string Left { get; private set; }

        /// <summary>
        /// Total filled in quote currency. Deprecated in favor of &#x60;filled_total&#x60;
        /// </summary>
        /// <value>Total filled in quote currency. Deprecated in favor of &#x60;filled_total&#x60;</value>
        [JsonProperty("fill_price")]
        public string FillPrice { get; private set; }

        /// <summary>
        /// Total filled in quote currency
        /// </summary>
        /// <value>Total filled in quote currency</value>
        [JsonProperty("filled_total")]
        public string FilledTotal { get; private set; }

        /// <summary>
        /// Fee deducted
        /// </summary>
        /// <value>Fee deducted</value>
        [JsonProperty("fee")]
        public string Fee { get; private set; }

        /// <summary>
        /// Fee currency unit
        /// </summary>
        /// <value>Fee currency unit</value>
        [JsonProperty("fee_currency")]
        public string FeeCurrency { get; private set; }

        /// <summary>
        /// Points used to deduct fee
        /// </summary>
        /// <value>Points used to deduct fee</value>
        [JsonProperty("point_fee")]
        public string PointFee { get; private set; }

        /// <summary>
        /// GT used to deduct fee
        /// </summary>
        /// <value>GT used to deduct fee</value>
        [JsonProperty("gt_fee")]
        public string GtFee { get; private set; }

        /// <summary>
        /// Whether GT fee discount is used
        /// </summary>
        /// <value>Whether GT fee discount is used</value>
        [JsonProperty("gt_discount")]
        public bool GtDiscount { get; private set; }

        /// <summary>
        /// Rebated fee
        /// </summary>
        /// <value>Rebated fee</value>
        [JsonProperty("rebated_fee")]
        public string RebatedFee { get; private set; }

        /// <summary>
        /// Rebated fee currency unit
        /// </summary>
        /// <value>Rebated fee currency unit</value>
        [JsonProperty("rebated_fee_currency")]
        public string RebatedFeeCurrency { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BatchOrder {\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Succeeded: ").Append(Succeeded).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  UpdateTime: ").Append(UpdateTime).Append("\n");
            sb.Append("  CreateTimeMs: ").Append(CreateTimeMs).Append("\n");
            sb.Append("  UpdateTimeMs: ").Append(UpdateTimeMs).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  CurrencyPair: ").Append(CurrencyPair).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Account: ").Append(Account).Append("\n");
            sb.Append("  Side: ").Append(Side).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  TimeInForce: ").Append(TimeInForce).Append("\n");
            sb.Append("  Iceberg: ").Append(Iceberg).Append("\n");
            sb.Append("  AutoBorrow: ").Append(AutoBorrow).Append("\n");
            sb.Append("  AutoRepay: ").Append(AutoRepay).Append("\n");
            sb.Append("  Left: ").Append(Left).Append("\n");
            sb.Append("  FillPrice: ").Append(FillPrice).Append("\n");
            sb.Append("  FilledTotal: ").Append(FilledTotal).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  FeeCurrency: ").Append(FeeCurrency).Append("\n");
            sb.Append("  PointFee: ").Append(PointFee).Append("\n");
            sb.Append("  GtFee: ").Append(GtFee).Append("\n");
            sb.Append("  GtDiscount: ").Append(GtDiscount).Append("\n");
            sb.Append("  RebatedFee: ").Append(RebatedFee).Append("\n");
            sb.Append("  RebatedFeeCurrency: ").Append(RebatedFeeCurrency).Append("\n");
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
            return this.Equals(input as BatchOrder);
        }

        /// <summary>
        /// Returns true if BatchOrder instances are equal
        /// </summary>
        /// <param name="input">Instance of BatchOrder to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BatchOrder input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.Succeeded == input.Succeeded ||
                    this.Succeeded.Equals(input.Succeeded)
                ) && 
                (
                    this.Label == input.Label ||
                    (this.Label != null &&
                    this.Label.Equals(input.Label))
                ) && 
                (
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CreateTime == input.CreateTime ||
                    (this.CreateTime != null &&
                    this.CreateTime.Equals(input.CreateTime))
                ) && 
                (
                    this.UpdateTime == input.UpdateTime ||
                    (this.UpdateTime != null &&
                    this.UpdateTime.Equals(input.UpdateTime))
                ) && 
                (
                    this.CreateTimeMs == input.CreateTimeMs ||
                    this.CreateTimeMs.Equals(input.CreateTimeMs)
                ) && 
                (
                    this.UpdateTimeMs == input.UpdateTimeMs ||
                    this.UpdateTimeMs.Equals(input.UpdateTimeMs)
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.CurrencyPair == input.CurrencyPair ||
                    (this.CurrencyPair != null &&
                    this.CurrencyPair.Equals(input.CurrencyPair))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.Account == input.Account ||
                    this.Account.Equals(input.Account)
                ) && 
                (
                    this.Side == input.Side ||
                    this.Side.Equals(input.Side)
                ) && 
                (
                    this.Amount == input.Amount ||
                    (this.Amount != null &&
                    this.Amount.Equals(input.Amount))
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                ) && 
                (
                    this.TimeInForce == input.TimeInForce ||
                    this.TimeInForce.Equals(input.TimeInForce)
                ) && 
                (
                    this.Iceberg == input.Iceberg ||
                    (this.Iceberg != null &&
                    this.Iceberg.Equals(input.Iceberg))
                ) && 
                (
                    this.AutoBorrow == input.AutoBorrow ||
                    this.AutoBorrow.Equals(input.AutoBorrow)
                ) && 
                (
                    this.AutoRepay == input.AutoRepay ||
                    this.AutoRepay.Equals(input.AutoRepay)
                ) && 
                (
                    this.Left == input.Left ||
                    (this.Left != null &&
                    this.Left.Equals(input.Left))
                ) && 
                (
                    this.FillPrice == input.FillPrice ||
                    (this.FillPrice != null &&
                    this.FillPrice.Equals(input.FillPrice))
                ) && 
                (
                    this.FilledTotal == input.FilledTotal ||
                    (this.FilledTotal != null &&
                    this.FilledTotal.Equals(input.FilledTotal))
                ) && 
                (
                    this.Fee == input.Fee ||
                    (this.Fee != null &&
                    this.Fee.Equals(input.Fee))
                ) && 
                (
                    this.FeeCurrency == input.FeeCurrency ||
                    (this.FeeCurrency != null &&
                    this.FeeCurrency.Equals(input.FeeCurrency))
                ) && 
                (
                    this.PointFee == input.PointFee ||
                    (this.PointFee != null &&
                    this.PointFee.Equals(input.PointFee))
                ) && 
                (
                    this.GtFee == input.GtFee ||
                    (this.GtFee != null &&
                    this.GtFee.Equals(input.GtFee))
                ) && 
                (
                    this.GtDiscount == input.GtDiscount ||
                    this.GtDiscount.Equals(input.GtDiscount)
                ) && 
                (
                    this.RebatedFee == input.RebatedFee ||
                    (this.RebatedFee != null &&
                    this.RebatedFee.Equals(input.RebatedFee))
                ) && 
                (
                    this.RebatedFeeCurrency == input.RebatedFeeCurrency ||
                    (this.RebatedFeeCurrency != null &&
                    this.RebatedFeeCurrency.Equals(input.RebatedFeeCurrency))
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
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                hashCode = hashCode * 59 + this.Succeeded.GetHashCode();
                if (this.Label != null)
                    hashCode = hashCode * 59 + this.Label.GetHashCode();
                if (this.Message != null)
                    hashCode = hashCode * 59 + this.Message.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.CreateTime != null)
                    hashCode = hashCode * 59 + this.CreateTime.GetHashCode();
                if (this.UpdateTime != null)
                    hashCode = hashCode * 59 + this.UpdateTime.GetHashCode();
                hashCode = hashCode * 59 + this.CreateTimeMs.GetHashCode();
                hashCode = hashCode * 59 + this.UpdateTimeMs.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.CurrencyPair != null)
                    hashCode = hashCode * 59 + this.CurrencyPair.GetHashCode();
                hashCode = hashCode * 59 + this.Type.GetHashCode();
                hashCode = hashCode * 59 + this.Account.GetHashCode();
                hashCode = hashCode * 59 + this.Side.GetHashCode();
                if (this.Amount != null)
                    hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                hashCode = hashCode * 59 + this.TimeInForce.GetHashCode();
                if (this.Iceberg != null)
                    hashCode = hashCode * 59 + this.Iceberg.GetHashCode();
                hashCode = hashCode * 59 + this.AutoBorrow.GetHashCode();
                hashCode = hashCode * 59 + this.AutoRepay.GetHashCode();
                if (this.Left != null)
                    hashCode = hashCode * 59 + this.Left.GetHashCode();
                if (this.FillPrice != null)
                    hashCode = hashCode * 59 + this.FillPrice.GetHashCode();
                if (this.FilledTotal != null)
                    hashCode = hashCode * 59 + this.FilledTotal.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                if (this.FeeCurrency != null)
                    hashCode = hashCode * 59 + this.FeeCurrency.GetHashCode();
                if (this.PointFee != null)
                    hashCode = hashCode * 59 + this.PointFee.GetHashCode();
                if (this.GtFee != null)
                    hashCode = hashCode * 59 + this.GtFee.GetHashCode();
                hashCode = hashCode * 59 + this.GtDiscount.GetHashCode();
                if (this.RebatedFee != null)
                    hashCode = hashCode * 59 + this.RebatedFee.GetHashCode();
                if (this.RebatedFeeCurrency != null)
                    hashCode = hashCode * 59 + this.RebatedFeeCurrency.GetHashCode();
                return hashCode;
            }
        }
    }

}
