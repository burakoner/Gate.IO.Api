namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// FuturesTrade
    /// </summary>
    
    public class FuturesTrade :  IEquatable<FuturesTrade>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesTrade" /> class.
        /// </summary>
        /// <param name="id">Trade ID.</param>
        /// <param name="createTime">Trading time.</param>
        /// <param name="createTimeMs">Trading time, with milliseconds set to 3 decimal places..</param>
        /// <param name="contract">Futures contract.</param>
        /// <param name="size">Trading size.</param>
        /// <param name="price">Trading price (quote currency).</param>
        public FuturesTrade(long id = default(long), double createTime = default(double), double createTimeMs = default(double), string contract = default(string), long size = default(long), string price = default(string))
        {
            this.Id = id;
            this.CreateTime = createTime;
            this.CreateTimeMs = createTimeMs;
            this.Contract = contract;
            this.Size = size;
            this.Price = price;
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
        /// Trading time, with milliseconds set to 3 decimal places.
        /// </summary>
        /// <value>Trading time, with milliseconds set to 3 decimal places.</value>
        [JsonProperty("create_time_ms")]
        public double CreateTimeMs { get; set; }

        /// <summary>
        /// Futures contract
        /// </summary>
        /// <value>Futures contract</value>
        [JsonProperty("contract")]
        public string Contract { get; set; }

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
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesTrade {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  CreateTimeMs: ").Append(CreateTimeMs).Append("\n");
            sb.Append("  Contract: ").Append(Contract).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
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
            return this.Equals(input as FuturesTrade);
        }

        /// <summary>
        /// Returns true if FuturesTrade instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesTrade to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesTrade input)
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
                    this.CreateTimeMs == input.CreateTimeMs ||
                    this.CreateTimeMs.Equals(input.CreateTimeMs)
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
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
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
                hashCode = hashCode * 59 + this.CreateTimeMs.GetHashCode();
                if (this.Contract != null)
                    hashCode = hashCode * 59 + this.Contract.GetHashCode();
                hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                return hashCode;
            }
        }
    }

}
