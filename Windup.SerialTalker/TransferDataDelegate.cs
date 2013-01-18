using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public delegate void TransferDataDelegate (Int32 data);
    public delegate void TransferDataByLinesDelegate (Int32[] data);
    public delegate void TransferListDelegate (List<Int32> list);
}
