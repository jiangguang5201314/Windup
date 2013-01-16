using System;

namespace Windup.SerialTalker
{
    public interface IAnalyzer
    {
        void GetDataFromSerialAgent (Int32 data);
        void GetData (Int32 data);
        void WriteData (byte[] what);
    }
}
