namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// MyFuturesTrade
    /// </summary>
    
    public class MyFuturesTrade :  IEquatable<MyFuturesTrade>
    {
        /// <summary>
        /// Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;
        /// </summary>
        /// <value>Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RoleEnum
        {
            /// <summary>
            /// Enum Taker for value: taker
            /// </summary>
            [EnumMember(Value = "taker")]
            Taker = 1,

            /// <summary>
            /// Enum Maker for value: maker
            /// </summary>
            [EnumMember(Value = "maker")]
            Maker = 2

        }

        /// <summary>
        /// Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;
        /// </summary>
        /// <value>Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;</value>
        [JsonProperty("role")]
        public RoleEnum? Role { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MyFuturesTrade" /> class.
        /// </summary>
        /// <param name="id">Trade ID.</param>
        /// <param name="createTime">Trading time.</param>
        /// <param name="contract">Futures contract.</param>
        /// <param name="orderId">Order ID related.</param>
        /// <param name="size">Trading size.</param>
        /// <param name="price">Trading price.</param>
        /// <param name="role">Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;.</param>
        /// <param name="text">User defined information.</param>
        /// <param name="fee">Fee deducted.</param>
        /// <param name="pointFee">Points used to deduct fee.</param>
        public MyFuturesTrade(long id = default(long), double createTime = default(double), string contract = default(string), string orderId = default(string), long size = default(long), string price = default(string), RoleEnum? role = default(RoleEnum?), string text = default(string), string fee = default(string), string pointFee = default(string))
        {
            this.Id = id;
            this.CreateTime = createTime;
            this.Contract = contract;
            this.OrderId = orderId;
            this.Size = size;
            this.Price = price;
            this.Role = role;
            this.Text = text;
            this.Fee = fee;
            this.PointFee = pointFee;
        }

        /// <summary>
        /// Trade ID
        /// </summary>
        /// <value>Trade ID</value>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Trading time
        /// </summary>
        /// <value>Trading time</value>
        [JsonProperty("create_time")]
        public double CreateTime { get; set; }

        /// <summary>
        /// Futures contract
        /// </summary>
        /// <value>Futures contract</value>
        [JsonProperty("contract")]
        public string Contract { get; set; }

        /// <summary>
        /// Order ID related
        /// </summary>
        /// <value>Order ID related</value>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Trading size
        /// </summary>
        /// <value>Trading size</value>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// Trading price
        /// </summary>
        /// <value>Trading price</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// User defined information
        /// </summary>
        /// <value>User defined information</value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Fee deducted
        /// </summary>
        /// <value>Fee deducted</value>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Points used to deduct fee
        /// </summary>
        /// <value>Points used to deduct fee</value>
        [JsonProperty("point_fee")]
        public string PointFee { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MyFuturesTrade {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  OrderId: ").Append(OrderId).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  PointFee: ").Append(PointFee).Append("\n");
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
            return this.Equals(input as MyFuturesTrade);
        }

        /// <summary>
        /// Returns true if MyFuturesTrade instances are equal
        /// </summary>
        /// <param name="input">Instance of MyFuturesTrade to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MyFuturesTrade input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.CreateTime == input.CreateTime ||
                    this.CreateTime.Equals(input.CreateTime)
                ) && 
                (
                    this.Contract == input.Contract ||
                    (this.Contract != null &&
                    this.Contract.Equals(input.Contract))
                ) && 
                (
                    this.OrderId == input.OrderId ||
                    (this.OrderId != null &&
                    this.OrderId.Equals(input.OrderId))
                ) && 
                (
                    this.Size == input.Size ||
                    this.Size.Equals(input.Size)
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                ) && 
                (
                    this.Role == input.Role ||
                    this.Role.Equals(input.Role)
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.Fee == input.Fee ||
                    (this.Fee != null &&
                    this.Fee.Equals(input.Fee))
                ) && 
                (
                    this.PointFee == input.PointFee ||
                    (this.PointFee != null &&
                    this.PointFee.Equals(input.PointFee))
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
                hashCode = hashCode * 59 + this.CreateTime.GetHashCode();
                if (this.Contract != null)
                    hashCode = hashCode * 59 + this.Contract.GetHashCode();
                if (this.OrderId != null)
                    hashCode = hashCode * 59 + this.OrderId.GetHashCode();
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                hashCode = hashCode * 59 + this.Role.GetHashCode();
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                if (this.PointFee != null)
                    hashCode = hashCode * 59 + this.PointFee.GetHashCode();
                return hashCode;
            }
        }
    }

}
