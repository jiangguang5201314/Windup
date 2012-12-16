using System;

namespace Windup.SerialTalker
{
    public delegate void TransferDataDelegate(Int32 ReceviedData);
	public delegate void TransferDataByLinesDelegate(Int32[] ReceviedData);
}
