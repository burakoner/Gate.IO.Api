namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// FuturesPriceTrigger
    /// </summary>
    
    public class FuturesPriceTrigger :  IEquatable<FuturesPriceTrigger>
    {
        /// <summary>
        /// How the order will be triggered   - &#x60;0&#x60;: by price, which means the order will be triggered if price condition is satisfied  - &#x60;1&#x60;: by price gap, which means the order will be triggered if gap of recent two prices of specified &#x60;price_type&#x60; are satisfied.  Only &#x60;0&#x60; is supported currently
        /// </summary>
        /// <value>How the order will be triggered   - &#x60;0&#x60;: by price, which means the order will be triggered if price condition is satisfied  - &#x60;1&#x60;: by price gap, which means the order will be triggered if gap of recent two prices of specified &#x60;price_type&#x60; are satisfied.  Only &#x60;0&#x60; is supported currently</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StrategyTypeEnum
        {
            /// <summary>
            /// Enum NUMBER_0 for value: 0
            /// </summary>
            NUMBER_0 = 0,

            /// <summary>
            /// Enum NUMBER_1 for value: 1
            /// </summary>
            NUMBER_1 = 1

        }

        /// <summary>
        /// How the order will be triggered   - &#x60;0&#x60;: by price, which means the order will be triggered if price condition is satisfied  - &#x60;1&#x60;: by price gap, which means the order will be triggered if gap of recent two prices of specified &#x60;price_type&#x60; are satisfied.  Only &#x60;0&#x60; is supported currently
        /// </summary>
        /// <value>How the order will be triggered   - &#x60;0&#x60;: by price, which means the order will be triggered if price condition is satisfied  - &#x60;1&#x60;: by price gap, which means the order will be triggered if gap of recent two prices of specified &#x60;price_type&#x60; are satisfied.  Only &#x60;0&#x60; is supported currently</value>
        [JsonProperty("strategy_type")]
        public StrategyTypeEnum? StrategyType { get; set; }
        /// <summary>
        /// Price type. 0 - latest deal price, 1 - mark price, 2 - index price
        /// </summary>
        /// <value>Price type. 0 - latest deal price, 1 - mark price, 2 - index price</value>
        public enum PriceTypeEnum
        {
            /// <summary>
            /// Enum NUMBER_0 for value: 0
            /// </summary>
            NUMBER_0 = 0,

            /// <summary>
            /// Enum NUMBER_1 for value: 1
            /// </summary>
            NUMBER_1 = 1,

            /// <summary>
            /// Enum NUMBER_2 for value: 2
            /// </summary>
            NUMBER_2 = 2

        }

        /// <summary>
        /// Price type. 0 - latest deal price, 1 - mark price, 2 - index price
        /// </summary>
        /// <value>Price type. 0 - latest deal price, 1 - mark price, 2 - index price</value>
        [JsonProperty("price_type")]
        public PriceTypeEnum? PriceType { get; set; }
        /// <summary>
        /// Trigger condition type  - &#x60;1&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &gt;&#x3D; &#x60;price&#x60; - &#x60;2&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &lt;&#x3D; &#x60;price&#x60;
        /// </summary>
        /// <value>Trigger condition type  - &#x60;1&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &gt;&#x3D; &#x60;price&#x60; - &#x60;2&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &lt;&#x3D; &#x60;price&#x60;</value>
        public enum RuleEnum
        {
            /// <summary>
            /// Enum NUMBER_1 for value: 1
            /// </summary>
            NUMBER_1 = 1,

            /// <summary>
            /// Enum NUMBER_2 for value: 2
            /// </summary>
            NUMBER_2 = 2

        }

        /// <summary>
        /// Trigger condition type  - &#x60;1&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &gt;&#x3D; &#x60;price&#x60; - &#x60;2&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &lt;&#x3D; &#x60;price&#x60;
        /// </summary>
        /// <value>Trigger condition type  - &#x60;1&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &gt;&#x3D; &#x60;price&#x60; - &#x60;2&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &lt;&#x3D; &#x60;price&#x60;</value>
        [JsonProperty("rule")]
        public RuleEnum? Rule { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FuturesPriceTrigger" /> class.
        /// </summary>
        /// <param name="strategyType">How the order will be triggered   - &#x60;0&#x60;: by price, which means the order will be triggered if price condition is satisfied  - &#x60;1&#x60;: by price gap, which means the order will be triggered if gap of recent two prices of specified &#x60;price_type&#x60; are satisfied.  Only &#x60;0&#x60; is supported currently.</param>
        /// <param name="priceType">Price type. 0 - latest deal price, 1 - mark price, 2 - index price.</param>
        /// <param name="price">Value of price on price triggered, or price gap on price gap triggered.</param>
        /// <param name="rule">Trigger condition type  - &#x60;1&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &gt;&#x3D; &#x60;price&#x60; - &#x60;2&#x60;: calculated price based on &#x60;strategy_type&#x60; and &#x60;price_type&#x60; &lt;&#x3D; &#x60;price&#x60;.</param>
        /// <param name="expiration">How long (in seconds) to wait for the condition to be triggered before cancelling the order..</param>
        public FuturesPriceTrigger(StrategyTypeEnum? strategyType = default(StrategyTypeEnum?), PriceTypeEnum? priceType = default(PriceTypeEnum?), string price = default(string), RuleEnum? rule = default(RuleEnum?), int expiration = default(int))
        {
            this.StrategyType = strategyType;
            this.PriceType = priceType;
            this.Price = price;
            this.Rule = rule;
            this.Expiration = expiration;
        }

        /// <summary>
        /// Value of price on price triggered, or price gap on price gap triggered
        /// </summary>
        /// <value>Value of price on price triggered, or price gap on price gap triggered</value>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// How long (in seconds) to wait for the condition to be triggered before cancelling the order.
        /// </summary>
        /// <value>How long (in seconds) to wait for the condition to be triggered before cancelling the order.</value>
        [JsonProperty("expiration")]
        public int Expiration { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FuturesPriceTrigger {\n");
            sb.Append("  StrategyType: ").Append(StrategyType).Append("\n");
            sb.Append("  PriceType: ").Append(PriceType).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Rule: ").Append(Rule).Append("\n");
            sb.Append("  Expiration: ").Append(Expiration).Append("\n");
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
            return this.Equals(input as FuturesPriceTrigger);
        }

        /// <summary>
        /// Returns true if FuturesPriceTrigger instances are equal
        /// </summary>
        /// <param name="input">Instance of FuturesPriceTrigger to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FuturesPriceTrigger input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.StrategyType == input.StrategyType ||
                    this.StrategyType.Equals(input.StrategyType)
                ) && 
                (
                    this.PriceType == input.PriceType ||
                    this.PriceType.Equals(input.PriceType)
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                ) && 
                (
                    this.Rule == input.Rule ||
                    this.Rule.Equals(input.Rule)
                ) && 
                (
                    this.Expiration == input.Expiration ||
                    this.Expiration.Equals(input.Expiration)
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
                hashCode = hashCode * 59 + this.StrategyType.GetHashCode();
                hashCode = hashCode * 59 + this.PriceType.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                hashCode = hashCode * 59 + this.Rule.GetHashCode();
                hashCode = hashCode * 59 + this.Expiration.GetHashCode();
                return hashCode;
            }
        }
    }

}
