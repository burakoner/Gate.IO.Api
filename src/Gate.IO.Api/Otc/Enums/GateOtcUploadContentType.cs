namespace Gate.IO.Api.Otc;

/// <summary>Supported pre-upload MIME types. The wire value is base64, not plaintext MIME.</summary>
public enum GateOtcUploadContentType : byte
{
    /// <summary>image/png</summary>
    [Map("aW1hZ2UvcG5n")]
    Png = 1,

    /// <summary>image/jpeg</summary>
    [Map("aW1hZ2UvanBlZw==")]
    Jpeg = 2,

    /// <summary>image/jpg</summary>
    [Map("aW1hZ2UvanBn")]
    Jpg = 3,

    /// <summary>application/pdf</summary>
    [Map("YXBwbGljYXRpb24vcGRm")]
    Pdf = 4,
}
