using System;

namespace Windup.SerialTalker
{
    public interface IAnalyzer
    {
        void BindListDelegate (TransferListDelegate tld);
        /* SerialAgent callback method TransferDataDelegate 
           or TransferDataByLinesDelegate
        */
        void TransferDelegate (Int32 data);
        void GetData (Int32 data);
        void WriteData (byte[] what);
    }
}
