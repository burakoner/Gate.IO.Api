namespace Gate.IO.Api.Otc;

/// <summary>
/// Personal OTC bank card supplement request
/// </summary>
[JsonConverter(typeof(GateOtcBankJsonConverter))]
public record GateOtcBankPersonalSupplementRequest
{
    /// <summary>
    /// Bank card ID
    /// </summary>
    [JsonProperty("bank_id", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string BankId { get; set; }

    /// <summary>
    /// Optional Base64 ID front content, decoded into a real file part. Excludes IdDocumentFrontUpload.
    /// </summary>
    [JsonProperty("id_document_front", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string IdDocumentFront { get; set; }

    /// <summary>Local raw ID front file input; not an extra HTTP field.</summary>
    [JsonProperty("id_document_front_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload IdDocumentFrontUpload { get; set; }

    /// <summary>
    /// Optional Base64 ID back content, decoded into a real file part. Excludes IdDocumentBackUpload.
    /// </summary>
    [JsonProperty("id_document_back", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string IdDocumentBack { get; set; }

    /// <summary>Local raw ID back file input; not an extra HTTP field.</summary>
    [JsonProperty("id_document_back_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload IdDocumentBackUpload { get; set; }

    /// <summary>
    /// Optional Base64 address proof content, decoded into a real file part. Excludes AddressProofUpload.
    /// </summary>
    [JsonProperty("address_proof", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string AddressProof { get; set; }

    /// <summary>Local raw address proof input; not an extra HTTP field.</summary>
    [JsonProperty("address_proof_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload AddressProofUpload { get; set; }

    /// <summary>
    /// Optional JSON text sent unchanged. Pre-upload items require plaintext object keys and MIME types,
    /// unlike bank/create and order/paid. Follow the current checklist; the client does not infer its JSON shape.
    /// </summary>
    [JsonProperty("relationship_proof", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string RelationshipProof { get; set; }
}
