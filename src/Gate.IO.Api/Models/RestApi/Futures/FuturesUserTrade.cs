namespace Gate.IO.Api.Models.RestApi.Futures;

public  class FuturesUserTrade
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("order_id")]
    public long OrderId { get; set; }
    
    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }
    
    [JsonProperty("point_fee")]
    public decimal PointFee { get; set; }
    
    [JsonProperty("role"), JsonConverter(typeof(FuturesTraderRoleConverter))]
    public FuturesTraderRole Role { get; set; }
}
