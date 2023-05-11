namespace Gate.IO.Api.Enums;

public enum MarginTransactionType
{
    [Label("in")]
    TransferIn, 

    [Label("out")]
    TransferOut,

    [Label("repay")]
    Repay,

    [Label("borrow")]
    Borrow,

    [Label("interest")]
    Interest,
    
    [Label("new_order")]
    NewOrder,
    
    [Label("order_fill")]
    OrderFill,
    
    [Label("referral_fee")]
    ReferralFee,
    
    [Label("order_fee")]
    OrderFee,
    
    [Label("unknown")]
    Unknown
}