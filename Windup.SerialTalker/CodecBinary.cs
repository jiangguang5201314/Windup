using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public class CodecBinary : ICodec
    {
        public TransferListDelegate tld;
        public IAnalyzer analyzer;
        public CodecBinary (IAnalyzer a)
        {
            analyzer = a;
            analyzer.BindListDelegate (this.TransfListFromAnalyzer);
        }

        public void TransfListFromAnalyzer (IList<Int32> list)
        {
        }

        public void EnCoder ()
        {
        }

        public void DeCoder ()
        {
        }
    }
}

