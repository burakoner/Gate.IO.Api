namespace Gate.IO.Api.Rebate;

/// <summary>
/// Partner application record
/// </summary>
public record GateRebatePartnerApplication
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("country_id")]
    public long CountryId { get; set; }

    [JsonProperty("firstname")]
    public string FirstName { get; set; }

    [JsonProperty("lastname")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("join_uid")]
    public long JoinUserId { get; set; }

    [JsonProperty("join_country_id")]
    public long JoinCountryId { get; set; }

    [JsonProperty("identity_comment")]
    public string IdentityComment { get; set; }

    [JsonProperty("promotion_channels")]
    public string PromotionChannels { get; set; }

    [JsonProperty("contact_details")]
    public string ContactDetails { get; set; }

    [JsonProperty("know_details")]
    public string KnowDetails { get; set; }

    [JsonProperty("question_lang")]
    public string QuestionLanguage { get; set; }

    [JsonProperty("create_timest")]
    public string CreateTime { get; set; }

    [JsonProperty("update_timest")]
    public string UpdateTime { get; set; }

    [JsonProperty("apply_type")]
    public int ApplyType { get; set; }

    [JsonProperty("audit_status")]
    public int AuditStatus { get; set; }

    [JsonProperty("edit_counts")]
    public int EditCount { get; set; }

    [JsonProperty("proof_images")]
    public string ProofImages { get; set; }

    [JsonProperty("proof_videos")]
    public string ProofVideos { get; set; }

    [JsonProperty("proof_url")]
    public string ProofUrl { get; set; }

    [JsonProperty("audit_reason")]
    public int AuditReason { get; set; }

    [JsonProperty("channel_type")]
    public int ChannelType { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("telegram")]
    public string Telegram { get; set; }

    [JsonProperty("other_contact")]
    public GateRebatePartnerApplicationContact OtherContact { get; set; }

    [JsonProperty("proof_images_url_list")]
    public List<string> ProofImageUrls { get; set; } = [];

    [JsonProperty("proof_videos_url_list")]
    public List<string> ProofVideoUrls { get; set; } = [];

    [JsonProperty("apply_msg")]
    public string ApplyMessage { get; set; }

    [JsonProperty("jump_url")]
    public string JumpUrl { get; set; }
}

/// <summary>
/// Partner application contact
/// </summary>
public record GateRebatePartnerApplicationContact
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}
