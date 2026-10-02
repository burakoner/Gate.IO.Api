namespace Gate.IO.Api.Otc;

/// <summary>Explicit OTC file usage; omission lets the server default to general.</summary>
public enum GateOtcUploadScene : byte
{
    /// <summary>Fiat buy payment receipt.</summary>
    [Map("general")]
    General = 1,

    /// <summary>Bank card binding or supplementary materials.</summary>
    [Map("bank")]
    Bank = 2,

    /// <summary>Professional verification materials.</summary>
    [Map("assessment")]
    Assessment = 3,

    /// <summary>Credit limit increase materials.</summary>
    [Map("credit")]
    Credit = 4,
}
