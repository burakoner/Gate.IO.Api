using System;
using System.Collections.Generic;
using System.Text;

namespace Gate.IO.Api.Extensions;

internal static class NumberExtensions
{
    public static CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    public static string ToGateString(this decimal @this)
    {
        return @this.ToString(CI);
    }
}
