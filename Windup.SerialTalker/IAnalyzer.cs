using System;

namespace Windup.SerialTalker
{
    public interface IAnalyzer
    {
        /* SerialAgent callback method TransferDataDelegate 
           or TransferDataByLinesDelegate
        */
        void TransferDelegate (Int32 data);
        void GetData (Int32 data);
        void WriteData (byte[] what);
    }
}
