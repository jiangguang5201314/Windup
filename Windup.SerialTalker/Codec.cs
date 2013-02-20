using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public class Codec
    {
        IDecoder decoder;
        IEncoder encoder;
        Analyzer analyzer;
        public Codec (ICodecFactory cf, Analyzer a)
        {
            decoder = cf.GetDecoder ();
            encoder = cf.GetEncoder ();
            analyzer = a;
            analyzer.BindListDelegate (this.GetDataFromAnalyzer);
        }

        public void GetDataFromAnalyzer (IList<Int32> data)
        {
            var dic = decoder.Decode (data);
            foreach (KeyValuePair<string, string> kvp in dic) {
                Console.WriteLine ("key={0}, value={1}", kvp.Key, kvp.Value);
            }
        }
    }
}

