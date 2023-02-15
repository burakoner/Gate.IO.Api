namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// TransactionID
    /// </summary>
    
    public class TransactionID :  IEquatable<TransactionID>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionID" /> class.
        /// </summary>
        /// <param name="txId">Order id.</param>
        public TransactionID(long txId = default(long))
        {
            this.TxId = txId;
        }

        /// <summary>
        /// Order id
        /// </summary>
        /// <value>Order id</value>
        [JsonProperty("tx_id")]
        public long TxId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TransactionID {\n");
            sb.Append("  TxId: ").Append(TxId).Append("\n");
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
            return this.Equals(input as TransactionID);
        }

        /// <summary>
        /// Returns true if TransactionID instances are equal
        /// </summary>
        /// <param name="input">Instance of TransactionID to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TransactionID input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.TxId == input.TxId ||
                    this.TxId.Equals(input.TxId)
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
                hashCode = hashCode * 59 + this.TxId.GetHashCode();
                return hashCode;
            }
        }
    }

}
