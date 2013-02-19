using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public abstract class Analyzer
    {
        protected List<Int32> tempList;
        protected TransferListDelegate transferListDelegate;
        protected SerialAgent s;
        public Analyzer (SerialAgent s)
        {
            tempList = new List<Int32> ();
            this.s = s;
            this.s.BindDataDelegate (this.TransferDelegate);
        }

        public void BindListDelegate (TransferListDelegate tld)
        {
            transferListDelegate = tld;
        }

        public void WriteData (byte[] what)
        {
            if (null == what)
                throw new ArgumentNullException ();
            s.AgentWrite (what);
        }

        void BreakDataStream ()
        {
            transferListDelegate (tempList);
            tempList = new List<Int32> ();
        }

        void TransferDelegate (Int32 data)
        {
            if (IsLineBreak (data))
                BreakDataStream ();
            else
                tempList.Add (data);
        }

        public abstract bool IsLineBreak (Int32 data);
    }
}

