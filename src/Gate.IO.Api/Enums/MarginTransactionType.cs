namespace Gate.IO.Api.Enums;

public enum MarginTransactionType
{
    [Map("in")]
    TransferIn, 

    [Map("out")]
    TransferOut,

    [Map("repay")]
    Repay,

    [Map("borrow")]
    Borrow,

    [Map("interest")]
    Interest,
    
    [Map("new_order")]
    NewOrder,
    
    [Map("order_fill")]
    OrderFill,
    
    [Map("referral_fee")]
    ReferralFee,
    
    [Map("order_fee")]
    OrderFee,
    
    [Map("unknown")]
    Unknown
}