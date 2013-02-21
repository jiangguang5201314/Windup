using System;

namespace Windup.SerialTalker
{
    public class AnalyzerNT : Analyzer
    {
        int flag = 0;
        public AnalyzerNT (SerialAgent s) : base(s)
        {
        }

        public override bool IsLineBreak (int data)
        {
            //throw new System.NotImplementedException ();
            if (13 == data) {
                flag = 1;
                return false;
            }
            if (1 == flag) {
                flag = 0;
                if (10 == data) {
                    base.tempList.RemoveAt (tempList.Count - 1);
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }
    }
}

