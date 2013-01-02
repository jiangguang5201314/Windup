using System;

namespace Windup.SerialTalker
{
    public interface IAnalyzer
    {
        void GetDataFromSerialAgent (Int32 data);
    }
}
