namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// OptionsAccountBook
    /// </summary>
    
    public class OptionsAccountBook :  IEquatable<OptionsAccountBook>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsAccountBook" /> class.
        /// </summary>
        /// <param name="time">Change time.</param>
        /// <param name="change">Amount changed (USDT).</param>
        /// <param name="balance">Account total balance after change (USDT).</param>
        /// <param name="type">Changing Type: - dnw: Deposit &amp; Withdraw - prem: Trading premium - fee: Trading fee - refr: Referrer rebate - point_dnw: POINT Deposit &amp; Withdraw - point_fee: POINT Trading fee - point_refr: POINT Referrer rebate.</param>
        /// <param name="text">custom text.</param>
        public OptionsAccountBook(double time = default(double), string change = default(string), string balance = default(string), string type = default(string), string text = default(string))
        {
            this.Time = time;
            this.Change = change;
            this.Balance = balance;
            this.Type = type;
            this.Text = text;
        }

        /// <summary>
        /// Change time
        /// </summary>
        /// <value>Change time</value>
        [JsonProperty("time")]
        public double Time { get; set; }

        /// <summary>
        /// Amount changed (USDT)
        /// </summary>
        /// <value>Amount changed (USDT)</value>
        [JsonProperty("change")]
        public string Change { get; set; }

        /// <summary>
        /// Account total balance after change (USDT)
        /// </summary>
        /// <value>Account total balance after change (USDT)</value>
        [JsonProperty("balance")]
        public string Balance { get; set; }

        /// <summary>
        /// Changing Type: - dnw: Deposit &amp; Withdraw - prem: Trading premium - fee: Trading fee - refr: Referrer rebate - point_dnw: POINT Deposit &amp; Withdraw - point_fee: POINT Trading fee - point_refr: POINT Referrer rebate
        /// </summary>
        /// <value>Changing Type: - dnw: Deposit &amp; Withdraw - prem: Trading premium - fee: Trading fee - refr: Referrer rebate - point_dnw: POINT Deposit &amp; Withdraw - point_fee: POINT Trading fee - point_refr: POINT Referrer rebate</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// custom text
        /// </summary>
        /// <value>custom text</value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OptionsAccountBook {\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  Change: ").Append(Change).Append("\n");
            sb.Append("  Balance: ").Append(Balance).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
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
            return this.Equals(input as OptionsAccountBook);
        }

        /// <summary>
        /// Returns true if OptionsAccountBook instances are equal
        /// </summary>
        /// <param name="input">Instance of OptionsAccountBook to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OptionsAccountBook input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Time == input.Time ||
                    this.Time.Equals(input.Time)
                ) && 
                (
                    this.Change == input.Change ||
                    (this.Change != null &&
                    this.Change.Equals(input.Change))
                ) && 
                (
                    this.Balance == input.Balance ||
                    (this.Balance != null &&
                    this.Balance.Equals(input.Balance))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
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
                hashCode = hashCode * 59 + this.Time.GetHashCode();
                if (this.Change != null)
                    hashCode = hashCode * 59 + this.Change.GetHashCode();
                if (this.Balance != null)
                    hashCode = hashCode * 59 + this.Balance.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                return hashCode;
            }
        }
    }

}
