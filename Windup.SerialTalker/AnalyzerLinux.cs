using System;

namespace Windup.SerialTalker
{
    public class AnalyzerLinux : Analyzer
    {
        public AnalyzerLinux (SerialAgent s) : base(s)
        {
        }

        public override bool IsLineBreak (int data)
        {
            bool result;
            result = (10 == data ? true : false);
            return result;
        }
    }
}

