using System;
using System.Collections;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public class AnalyzerWindows : IAnalyzer
    {
        List<Int32> tempList;
        /// <summary>
        /// The line break flag. if prev data is 13('\r') thus the lineBreakFlag == 1
        /// else lineBreakFlag == 0
        /// </summary>
        int lineBreakFlag = 0;

        bool IsLineBreak (Int32 data)
        {
            if (13 == data) {
                lineBreakFlag = 1;
                return false;
            }

            if (1 == lineBreakFlag) {
                lineBreakFlag = 0;
                if (10 == data) {
                    return true;
                } else {
                    return false;
                }
            }

            return false;
        }

        public AnalyzerWindows ()
        {
            tempList = new List<Int32> ();
        }

        public void GetData (Int32 data)
        {
        }

        public void WriteData (byte[] what)
        {
            if (what == null)
                throw new ArgumentNullException ("what");
        }

        public void TransferDelegate (Int32 data)
        {
            tempList.Add (data);
        }
    }
}

