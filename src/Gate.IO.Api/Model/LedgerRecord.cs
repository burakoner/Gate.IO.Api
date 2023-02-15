namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// LedgerRecord
    /// </summary>
    
    public class LedgerRecord :  IEquatable<LedgerRecord>
    {
        /// <summary>
        /// Record status.  - DONE: done - CANCEL: cancelled - REQUEST: requesting - MANUAL: pending manual approval - BCODE: GateCode operation - EXTPEND: pending confirm after sending - FAIL: pending confirm when fail - INVALID: invalid order - VERIFY: verifying - PROCES: processing - PEND: pending - DMOVE: required manual approval - SPLITPEND: the order is automatically split due to large amount
        /// </summary>
        /// <value>Record status.  - DONE: done - CANCEL: cancelled - REQUEST: requesting - MANUAL: pending manual approval - BCODE: GateCode operation - EXTPEND: pending confirm after sending - FAIL: pending confirm when fail - INVALID: invalid order - VERIFY: verifying - PROCES: processing - PEND: pending - DMOVE: required manual approval - SPLITPEND: the order is automatically split due to large amount</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum DONE for value: DONE
            /// </summary>
            [EnumMember(Value = "DONE")]
            DONE = 1,

            /// <summary>
            /// Enum CANCEL for value: CANCEL
            /// </summary>
            [EnumMember(Value = "CANCEL")]
            CANCEL = 2,

            /// <summary>
            /// Enum REQUEST for value: REQUEST
            /// </summary>
            [EnumMember(Value = "REQUEST")]
            REQUEST = 3,

            /// <summary>
            /// Enum MANUAL for value: MANUAL
            /// </summary>
            [EnumMember(Value = "MANUAL")]
            MANUAL = 4,

            /// <summary>
            /// Enum BCODE for value: BCODE
            /// </summary>
            [EnumMember(Value = "BCODE")]
            BCODE = 5,

            /// <summary>
            /// Enum EXTPEND for value: EXTPEND
            /// </summary>
            [EnumMember(Value = "EXTPEND")]
            EXTPEND = 6,

            /// <summary>
            /// Enum FAIL for value: FAIL
            /// </summary>
            [EnumMember(Value = "FAIL")]
            FAIL = 7,

            /// <summary>
            /// Enum INVALID for value: INVALID
            /// </summary>
            [EnumMember(Value = "INVALID")]
            INVALID = 8,

            /// <summary>
            /// Enum VERIFY for value: VERIFY
            /// </summary>
            [EnumMember(Value = "VERIFY")]
            VERIFY = 9,

            /// <summary>
            /// Enum PROCES for value: PROCES
            /// </summary>
            [EnumMember(Value = "PROCES")]
            PROCES = 10,

            /// <summary>
            /// Enum PEND for value: PEND
            /// </summary>
            [EnumMember(Value = "PEND")]
            PEND = 11,

            /// <summary>
            /// Enum DMOVE for value: DMOVE
            /// </summary>
            [EnumMember(Value = "DMOVE")]
            DMOVE = 12,

            /// <summary>
            /// Enum SPLITPEND for value: SPLITPEND
            /// </summary>
            [EnumMember(Value = "SPLITPEND")]
            SPLITPEND = 13

        }

        /// <summary>
        /// Record status.  - DONE: done - CANCEL: cancelled - REQUEST: requesting - MANUAL: pending manual approval - BCODE: GateCode operation - EXTPEND: pending confirm after sending - FAIL: pending confirm when fail - INVALID: invalid order - VERIFY: verifying - PROCES: processing - PEND: pending - DMOVE: required manual approval - SPLITPEND: the order is automatically split due to large amount
        /// </summary>
        /// <value>Record status.  - DONE: done - CANCEL: cancelled - REQUEST: requesting - MANUAL: pending manual approval - BCODE: GateCode operation - EXTPEND: pending confirm after sending - FAIL: pending confirm when fail - INVALID: invalid order - VERIFY: verifying - PROCES: processing - PEND: pending - DMOVE: required manual approval - SPLITPEND: the order is automatically split due to large amount</value>
        [JsonProperty("status")]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LedgerRecord" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected LedgerRecord() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="LedgerRecord" /> class.
        /// </summary>
        /// <param name="amount">Currency amount (required).</param>
        /// <param name="currency">Currency name (required).</param>
        /// <param name="address">Withdrawal address. Required for withdrawals.</param>
        /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
        /// <param name="chain">Name of the chain used in withdrawals (required).</param>
        public LedgerRecord(string amount = default(string), string currency = default(string), string address = default(string), string memo = default(string), string chain = default(string))
        {
            // to ensure "amount" is required (not null)
            this.Amount = amount ?? throw new ArgumentNullException("amount", "amount is a required property for LedgerRecord and cannot be null");
            // to ensure "currency" is required (not null)
            this.Currency = currency ?? throw new ArgumentNullException("currency", "currency is a required property for LedgerRecord and cannot be null");
            // to ensure "chain" is required (not null)
            this.Chain = chain ?? throw new ArgumentNullException("chain", "chain is a required property for LedgerRecord and cannot be null");
            this.Address = address;
            this.Memo = memo;
        }

        /// <summary>
        /// Record ID
        /// </summary>
        /// <value>Record ID</value>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// Hash record of the withdrawal
        /// </summary>
        /// <value>Hash record of the withdrawal</value>
        [JsonProperty("txid")]
        public string Txid { get; private set; }

        /// <summary>
        /// Operation time
        /// </summary>
        /// <value>Operation time</value>
        [JsonProperty("timestamp")]
        public string Timestamp { get; private set; }

        /// <summary>
        /// Currency amount
        /// </summary>
        /// <value>Currency amount</value>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Currency name
        /// </summary>
        /// <value>Currency name</value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Withdrawal address. Required for withdrawals
        /// </summary>
        /// <value>Withdrawal address. Required for withdrawals</value>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Additional remarks with regards to the withdrawal
        /// </summary>
        /// <value>Additional remarks with regards to the withdrawal</value>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// Name of the chain used in withdrawals
        /// </summary>
        /// <value>Name of the chain used in withdrawals</value>
        [JsonProperty("chain")]
        public string Chain { get; set; }

        /// <summary>
        /// Fee
        /// </summary>
        /// <value>Fee</value>
        [JsonProperty("fee")]
        public string Fee { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LedgerRecord {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Txid: ").Append(Txid).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  Memo: ").Append(Memo).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Chain: ").Append(Chain).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
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
            return this.Equals(input as LedgerRecord);
        }

        /// <summary>
        /// Returns true if LedgerRecord instances are equal
        /// </summary>
        /// <param name="input">Instance of LedgerRecord to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LedgerRecord input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Txid == input.Txid ||
                    (this.Txid != null &&
                    this.Txid.Equals(input.Txid))
                ) && 
                (
                    this.Timestamp == input.Timestamp ||
                    (this.Timestamp != null &&
                    this.Timestamp.Equals(input.Timestamp))
                ) && 
                (
                    this.Amount == input.Amount ||
                    (this.Amount != null &&
                    this.Amount.Equals(input.Amount))
                ) && 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.Address == input.Address ||
                    (this.Address != null &&
                    this.Address.Equals(input.Address))
                ) && 
                (
                    this.Memo == input.Memo ||
                    (this.Memo != null &&
                    this.Memo.Equals(input.Memo))
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.Chain == input.Chain ||
                    (this.Chain != null &&
                    this.Chain.Equals(input.Chain))
                ) && 
                (
                    this.Fee == input.Fee ||
                    (this.Fee != null &&
                    this.Fee.Equals(input.Fee))
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
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Txid != null)
                    hashCode = hashCode * 59 + this.Txid.GetHashCode();
                if (this.Timestamp != null)
                    hashCode = hashCode * 59 + this.Timestamp.GetHashCode();
                if (this.Amount != null)
                    hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                if (this.Address != null)
                    hashCode = hashCode * 59 + this.Address.GetHashCode();
                if (this.Memo != null)
                    hashCode = hashCode * 59 + this.Memo.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.Chain != null)
                    hashCode = hashCode * 59 + this.Chain.GetHashCode();
                if (this.Fee != null)
                    hashCode = hashCode * 59 + this.Fee.GetHashCode();
                return hashCode;
            }
        }
    }

}
