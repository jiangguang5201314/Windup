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
        TransferListDelegate transferListDelegate;
        public SerialAgent sa = null;

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

        void BreakDataStream ()
        {
            transferListDelegate (tempList);
            tempList = new List<Int32> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Windup.SerialTalker.AnalyzerWindows"/> class.
        /// you must initialize serialagent instance
        /// var a = new AnalyzerWindows();
        /// a.sa.AgentName = "COM1";
        /// ...
        /// </summary>
        public AnalyzerWindows ()
        {
            tempList = new List<Int32> ();
            sa = new SerialAgent ();
            sa.BindDataDelegate (this.TransferDelegate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Windup.SerialTalker.AnalyzerWindows"/> class.
        /// just afferent a SerialAgent instance
        /// </summary>
        /// <param name='s'>
        /// S.already initialized on external
        /// </param>
        public AnalyzerWindows (SerialAgent s)
        {
            tempList = new List<Int32> ();
            sa = s;
            sa.BindDataDelegate (this.TransferDelegate);
        }

        public void BindListDelegate (TransferListDelegate tld)
        {
            transferListDelegate = tld;
        }

        public void GetData (Int32 data)
        {
        }

        //write data to SerialAgent
        public void WriteData (byte[] what)
        {
            if (what == null)
                throw new ArgumentNullException ("what is null");
            sa.AgentWrite (what);
        }

        //for SerialAgent
        public void TransferDelegate (Int32 data)
        {
            if (IsLineBreak (data))
                BreakDataStream ();
            else
                tempList.Add (data);
        }
    }
}

