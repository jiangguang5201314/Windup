using System;

namespace Windup.SerialTalker
{
    public class AnalyzerMac : Analyzer
    {
        public AnalyzerMac (SerialAgent s) : base(s)
        {
        }

        public override bool IsLineBreak (int data)
        {
            bool result;
            result = (13 == data ? true : false);
            return result;
        }
    }
}

