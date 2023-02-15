namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// WithdrawStatus
    /// </summary>
    
    public class WithdrawStatus :  IEquatable<WithdrawStatus>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawStatus" /> class.
        /// </summary>
        /// <param name="currency">Currency.</param>
        /// <param name="name">Currency name.</param>
        /// <param name="nameCn">Currency Chinese name.</param>
        /// <param name="deposit">Deposits fee.</param>
        /// <param name="withdrawPercent">Withdrawal fee rate percentage.</param>
        /// <param name="withdrawFix">Fixed withdrawal fee.</param>
        /// <param name="withdrawDayLimit">Daily allowed withdrawal amount.</param>
        /// <param name="withdrawAmountMini">Minimum withdrawal amount.</param>
        /// <param name="withdrawDayLimitRemain">Daily withdrawal amount left.</param>
        /// <param name="withdrawEachtimeLimit">Maximum amount for each withdrawal.</param>
        /// <param name="withdrawFixOnChains">Fixed withdrawal fee on multiple chains.</param>
        public WithdrawStatus(string currency = default(string), string name = default(string), string nameCn = default(string), string deposit = default(string), string withdrawPercent = default(string), string withdrawFix = default(string), string withdrawDayLimit = default(string), string withdrawAmountMini = default(string), string withdrawDayLimitRemain = default(string), string withdrawEachtimeLimit = default(string), Dictionary<string, string> withdrawFixOnChains = default(Dictionary<string, string>))
        {
            this.Currency = currency;
            this.Name = name;
            this.NameCn = nameCn;
            this.Deposit = deposit;
            this.WithdrawPercent = withdrawPercent;
            this.WithdrawFix = withdrawFix;
            this.WithdrawDayLimit = withdrawDayLimit;
            this.WithdrawAmountMini = withdrawAmountMini;
            this.WithdrawDayLimitRemain = withdrawDayLimitRemain;
            this.WithdrawEachtimeLimit = withdrawEachtimeLimit;
            this.WithdrawFixOnChains = withdrawFixOnChains;
        }

        /// <summary>
        /// Currency
        /// </summary>
        /// <value>Currency</value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Currency name
        /// </summary>
        /// <value>Currency name</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Currency Chinese name
        /// </summary>
        /// <value>Currency Chinese name</value>
        [JsonProperty("name_cn")]
        public string NameCn { get; set; }

        /// <summary>
        /// Deposits fee
        /// </summary>
        /// <value>Deposits fee</value>
        [JsonProperty("deposit")]
        public string Deposit { get; set; }

        /// <summary>
        /// Withdrawal fee rate percentage
        /// </summary>
        /// <value>Withdrawal fee rate percentage</value>
        [JsonProperty("withdraw_percent")]
        public string WithdrawPercent { get; set; }

        /// <summary>
        /// Fixed withdrawal fee
        /// </summary>
        /// <value>Fixed withdrawal fee</value>
        [JsonProperty("withdraw_fix")]
        public string WithdrawFix { get; set; }

        /// <summary>
        /// Daily allowed withdrawal amount
        /// </summary>
        /// <value>Daily allowed withdrawal amount</value>
        [JsonProperty("withdraw_day_limit")]
        public string WithdrawDayLimit { get; set; }

        /// <summary>
        /// Minimum withdrawal amount
        /// </summary>
        /// <value>Minimum withdrawal amount</value>
        [JsonProperty("withdraw_amount_mini")]
        public string WithdrawAmountMini { get; set; }

        /// <summary>
        /// Daily withdrawal amount left
        /// </summary>
        /// <value>Daily withdrawal amount left</value>
        [JsonProperty("withdraw_day_limit_remain")]
        public string WithdrawDayLimitRemain { get; set; }

        /// <summary>
        /// Maximum amount for each withdrawal
        /// </summary>
        /// <value>Maximum amount for each withdrawal</value>
        [JsonProperty("withdraw_eachtime_limit")]
        public string WithdrawEachtimeLimit { get; set; }

        /// <summary>
        /// Fixed withdrawal fee on multiple chains
        /// </summary>
        /// <value>Fixed withdrawal fee on multiple chains</value>
        [JsonProperty("withdraw_fix_on_chains")]
        public Dictionary<string, string> WithdrawFixOnChains { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class WithdrawStatus {\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  NameCn: ").Append(NameCn).Append("\n");
            sb.Append("  Deposit: ").Append(Deposit).Append("\n");
            sb.Append("  WithdrawPercent: ").Append(WithdrawPercent).Append("\n");
            sb.Append("  WithdrawFix: ").Append(WithdrawFix).Append("\n");
            sb.Append("  WithdrawDayLimit: ").Append(WithdrawDayLimit).Append("\n");
            sb.Append("  WithdrawAmountMini: ").Append(WithdrawAmountMini).Append("\n");
            sb.Append("  WithdrawDayLimitRemain: ").Append(WithdrawDayLimitRemain).Append("\n");
            sb.Append("  WithdrawEachtimeLimit: ").Append(WithdrawEachtimeLimit).Append("\n");
            sb.Append("  WithdrawFixOnChains: ").Append(WithdrawFixOnChains).Append("\n");
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
            return this.Equals(input as WithdrawStatus);
        }

        /// <summary>
        /// Returns true if WithdrawStatus instances are equal
        /// </summary>
        /// <param name="input">Instance of WithdrawStatus to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WithdrawStatus input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.NameCn == input.NameCn ||
                    (this.NameCn != null &&
                    this.NameCn.Equals(input.NameCn))
                ) && 
                (
                    this.Deposit == input.Deposit ||
                    (this.Deposit != null &&
                    this.Deposit.Equals(input.Deposit))
                ) && 
                (
                    this.WithdrawPercent == input.WithdrawPercent ||
                    (this.WithdrawPercent != null &&
                    this.WithdrawPercent.Equals(input.WithdrawPercent))
                ) && 
                (
                    this.WithdrawFix == input.WithdrawFix ||
                    (this.WithdrawFix != null &&
                    this.WithdrawFix.Equals(input.WithdrawFix))
                ) && 
                (
                    this.WithdrawDayLimit == input.WithdrawDayLimit ||
                    (this.WithdrawDayLimit != null &&
                    this.WithdrawDayLimit.Equals(input.WithdrawDayLimit))
                ) && 
                (
                    this.WithdrawAmountMini == input.WithdrawAmountMini ||
                    (this.WithdrawAmountMini != null &&
                    this.WithdrawAmountMini.Equals(input.WithdrawAmountMini))
                ) && 
                (
                    this.WithdrawDayLimitRemain == input.WithdrawDayLimitRemain ||
                    (this.WithdrawDayLimitRemain != null &&
                    this.WithdrawDayLimitRemain.Equals(input.WithdrawDayLimitRemain))
                ) && 
                (
                    this.WithdrawEachtimeLimit == input.WithdrawEachtimeLimit ||
                    (this.WithdrawEachtimeLimit != null &&
                    this.WithdrawEachtimeLimit.Equals(input.WithdrawEachtimeLimit))
                ) && 
                (
                    this.WithdrawFixOnChains == input.WithdrawFixOnChains ||
                    this.WithdrawFixOnChains != null &&
                    input.WithdrawFixOnChains != null &&
                    this.WithdrawFixOnChains.SequenceEqual(input.WithdrawFixOnChains)
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
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.NameCn != null)
                    hashCode = hashCode * 59 + this.NameCn.GetHashCode();
                if (this.Deposit != null)
                    hashCode = hashCode * 59 + this.Deposit.GetHashCode();
                if (this.WithdrawPercent != null)
                    hashCode = hashCode * 59 + this.WithdrawPercent.GetHashCode();
                if (this.WithdrawFix != null)
                    hashCode = hashCode * 59 + this.WithdrawFix.GetHashCode();
                if (this.WithdrawDayLimit != null)
                    hashCode = hashCode * 59 + this.WithdrawDayLimit.GetHashCode();
                if (this.WithdrawAmountMini != null)
                    hashCode = hashCode * 59 + this.WithdrawAmountMini.GetHashCode();
                if (this.WithdrawDayLimitRemain != null)
                    hashCode = hashCode * 59 + this.WithdrawDayLimitRemain.GetHashCode();
                if (this.WithdrawEachtimeLimit != null)
                    hashCode = hashCode * 59 + this.WithdrawEachtimeLimit.GetHashCode();
                if (this.WithdrawFixOnChains != null)
                    hashCode = hashCode * 59 + this.WithdrawFixOnChains.GetHashCode();
                return hashCode;
            }
        }
    }

}
