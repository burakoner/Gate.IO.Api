namespace Io.Gate.GateApi.Model
{
    /// <summary>
    /// SavedAddress
    /// </summary>
    
    public class SavedAddress :  IEquatable<SavedAddress>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavedAddress" /> class.
        /// </summary>
        /// <param name="currency">Currency.</param>
        /// <param name="chain">Chain name.</param>
        /// <param name="address">Address.</param>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="verified">Whether to pass the verification 0-unverified, 1-verified.</param>
        public SavedAddress(string currency = default(string), string chain = default(string), string address = default(string), string name = default(string), string tag = default(string), string verified = default(string))
        {
            this.Currency = currency;
            this.Chain = chain;
            this.Address = address;
            this.Name = name;
            this.Tag = tag;
            this.Verified = verified;
        }

        /// <summary>
        /// Currency
        /// </summary>
        /// <value>Currency</value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Chain name
        /// </summary>
        /// <value>Chain name</value>
        [JsonProperty("chain")]
        public string Chain { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        /// <value>Address</value>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        /// <value>Tag</value>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Whether to pass the verification 0-unverified, 1-verified
        /// </summary>
        /// <value>Whether to pass the verification 0-unverified, 1-verified</value>
        [JsonProperty("verified")]
        public string Verified { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SavedAddress {\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Chain: ").Append(Chain).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Tag: ").Append(Tag).Append("\n");
            sb.Append("  Verified: ").Append(Verified).Append("\n");
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
            return this.Equals(input as SavedAddress);
        }

        /// <summary>
        /// Returns true if SavedAddress instances are equal
        /// </summary>
        /// <param name="input">Instance of SavedAddress to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SavedAddress input)
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
                    this.Chain == input.Chain ||
                    (this.Chain != null &&
                    this.Chain.Equals(input.Chain))
                ) && 
                (
                    this.Address == input.Address ||
                    (this.Address != null &&
                    this.Address.Equals(input.Address))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Tag == input.Tag ||
                    (this.Tag != null &&
                    this.Tag.Equals(input.Tag))
                ) && 
                (
                    this.Verified == input.Verified ||
                    (this.Verified != null &&
                    this.Verified.Equals(input.Verified))
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
                if (this.Chain != null)
                    hashCode = hashCode * 59 + this.Chain.GetHashCode();
                if (this.Address != null)
                    hashCode = hashCode * 59 + this.Address.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Tag != null)
                    hashCode = hashCode * 59 + this.Tag.GetHashCode();
                if (this.Verified != null)
                    hashCode = hashCode * 59 + this.Verified.GetHashCode();
                return hashCode;
            }
        }
    }

}
