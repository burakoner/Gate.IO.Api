namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsMyTrade
    /// </summary>
    
    public class OptionsMyTrade :  IEquatable<OptionsMyTrade>
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
        /// Initializes a new instance of the <see cref="OptionsMyTrade" /> class.
        /// </summary>
        /// <param name="id">Trade ID.</param>
        /// <param name="createTime">Trading time.</param>
        /// <param name="contract">Options contract name.</param>
        /// <param name="orderId">Order ID related.</param>
        /// <param name="size">Trading size.</param>
        /// <param name="price">Trading price (quote currency).</param>
        /// <param name="underlyingPrice">Underlying price (quote currency).</param>
        /// <param name="role">Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;.</param>
        public OptionsMyTrade(long id = default(long), double createTime = default(double), string contract = default(string), int orderId = default(int), long size = default(long), string price = default(string), string underlyingPrice = default(string), RoleEnum? role = default(RoleEnum?))
        {
            this.Id = id;
            this.CreateTime = createTime;
            this.Contract = contract;
            this.OrderId = orderId;
            this.Size = size;
            this.Price = price;
            this.UnderlyingPrice = underlyingPrice;
            this.Role = role;
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
        /// Options contract name
        /// </summary>
        /// <value>Options contract name</value>
        [JsonProperty("contract")]
        public string Contract { get; set; }

        /// <summary>
        /// Order ID related
        /// </summary>
        /// <value>Order ID related</value>
        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        /// <summary>
        /// Trading size
        /// </summary>
        /// <value>Trading size</value>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// Trading price (quote currency)
        /// </summary>
        /// <value>Trading price (quote currency)</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Underlying price (quote currency)
        /// </summary>
        /// <value>Underlying price (quote currency)</value>
        [JsonProperty("underlying_price")]
        public string UnderlyingPrice { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsMyTrade {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  OrderId: ").Append(OrderId).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  UnderlyingPrice: ").Append(UnderlyingPrice).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
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
            return this.Equals(input as OptionsMyTrade);
        }

        /// <summary>
        /// Returns true if OptionsMyTrade instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsMyTrade to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsMyTrade input)
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
                    this.OrderId.Equals(input.OrderId)
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
                    this.UnderlyingPrice == input.UnderlyingPrice ||
                    (this.UnderlyingPrice != null &&
                    this.UnderlyingPrice.Equals(input.UnderlyingPrice))
                ) && 
                (
                    this.Role == input.Role ||
                    this.Role.Equals(input.Role)
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
                hashCode = hashCode * 59 + this.OrderId.GetHashCode();
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                if (this.UnderlyingPrice != null)
                    hashCode = hashCode * 59 + this.UnderlyingPrice.GetHashCode();
                hashCode = hashCode * 59 + this.Role.GetHashCode();
                return hashCode;
            }
        }
    }

}
