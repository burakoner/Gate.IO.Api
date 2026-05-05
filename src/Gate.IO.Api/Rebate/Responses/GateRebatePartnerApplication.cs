namespace Gate.IO.Api.Rebate;

/// <summary>
/// Partner application record
/// </summary>
public record GateRebatePartnerApplication
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Language.
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; }

    /// <summary>
    /// Gets or sets the Country ID.
    /// </summary>
    [JsonProperty("country_id")]
    public long CountryId { get; set; }

    /// <summary>
    /// Gets or sets the First Name.
    /// </summary>
    [JsonProperty("firstname")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the Last Name.
    /// </summary>
    [JsonProperty("lastname")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the Join User ID.
    /// </summary>
    [JsonProperty("join_uid")]
    public long JoinUserId { get; set; }

    /// <summary>
    /// Gets or sets the Join Country ID.
    /// </summary>
    [JsonProperty("join_country_id")]
    public long JoinCountryId { get; set; }

    /// <summary>
    /// Gets or sets the Identity Comment.
    /// </summary>
    [JsonProperty("identity_comment")]
    public string IdentityComment { get; set; }

    /// <summary>
    /// Gets or sets the Promotion Channels.
    /// </summary>
    [JsonProperty("promotion_channels")]
    public string PromotionChannels { get; set; }

    /// <summary>
    /// Gets or sets the Contact Details.
    /// </summary>
    [JsonProperty("contact_details")]
    public string ContactDetails { get; set; }

    /// <summary>
    /// Gets or sets the Know Details.
    /// </summary>
    [JsonProperty("know_details")]
    public string KnowDetails { get; set; }

    /// <summary>
    /// Gets or sets the Question Language.
    /// </summary>
    [JsonProperty("question_lang")]
    public string QuestionLanguage { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_timest")]
    public string CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Update Time.
    /// </summary>
    [JsonProperty("update_timest")]
    public string UpdateTime { get; set; }

    /// <summary>
    /// Gets or sets the Apply Type.
    /// </summary>
    [JsonProperty("apply_type")]
    public int ApplyType { get; set; }

    /// <summary>
    /// Gets or sets the Audit Status.
    /// </summary>
    [JsonProperty("audit_status")]
    public int AuditStatus { get; set; }

    /// <summary>
    /// Gets or sets the Edit Count.
    /// </summary>
    [JsonProperty("edit_counts")]
    public int EditCount { get; set; }

    /// <summary>
    /// Gets or sets the Proof Images.
    /// </summary>
    [JsonProperty("proof_images")]
    public string ProofImages { get; set; }

    /// <summary>
    /// Gets or sets the Proof Videos.
    /// </summary>
    [JsonProperty("proof_videos")]
    public string ProofVideos { get; set; }

    /// <summary>
    /// Gets or sets the Proof URL.
    /// </summary>
    [JsonProperty("proof_url")]
    public string ProofUrl { get; set; }

    /// <summary>
    /// Gets or sets the Audit Reason.
    /// </summary>
    [JsonProperty("audit_reason")]
    public int AuditReason { get; set; }

    /// <summary>
    /// Gets or sets the Channel Type.
    /// </summary>
    [JsonProperty("channel_type")]
    public int ChannelType { get; set; }

    /// <summary>
    /// Gets or sets the Region.
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the Phone.
    /// </summary>
    [JsonProperty("phone")]
    public string Phone { get; set; }

    /// <summary>
    /// Gets or sets the Telegram.
    /// </summary>
    [JsonProperty("telegram")]
    public string Telegram { get; set; }

    /// <summary>
    /// Gets or sets the Other Contact.
    /// </summary>
    [JsonProperty("other_contact")]
    public GateRebatePartnerApplicationContact OtherContact { get; set; }

    /// <summary>
    /// Gets or sets the Proof Image Urls.
    /// </summary>
    [JsonProperty("proof_images_url_list")]
    public List<string> ProofImageUrls { get; set; } = [];

    /// <summary>
    /// Gets or sets the Proof Video Urls.
    /// </summary>
    [JsonProperty("proof_videos_url_list")]
    public List<string> ProofVideoUrls { get; set; } = [];

    /// <summary>
    /// Gets or sets the Apply Message.
    /// </summary>
    [JsonProperty("apply_msg")]
    public string ApplyMessage { get; set; }

    /// <summary>
    /// Gets or sets the Jump URL.
    /// </summary>
    [JsonProperty("jump_url")]
    public string JumpUrl { get; set; }
}

/// <summary>
/// Partner application contact
/// </summary>
public record GateRebatePartnerApplicationContact
{
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }

    /// <summary>
    /// Gets or sets the Value.
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }
}
