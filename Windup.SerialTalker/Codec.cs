using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public class Codec
    {
        IDecoder decoder;
        IEncoder encoder;
        Analyzer analyzer;

        public Queue<Dictionary<string, string>> queue = new Queue<Dictionary<string, string>> ();
        public Codec (ICodecFactory cf, Analyzer a)
        {
            decoder = cf.GetDecoder ();
            encoder = cf.GetEncoder ();
            analyzer = a;
            analyzer.BindListDelegate (this.GetDataFromAnalyzer);
        }

        public void WriteData (string what)
        {
            if (null == what)
                throw new ArgumentNullException ("what is null");
            analyzer.WriteData (encoder.Encode (what));
        }

        public void GetDataFromAnalyzer (IList<Int32> data)
        {
            var dic = decoder.Decode (data);
            queue.Enqueue (dic);

            foreach (KeyValuePair<string, string> kvp in dic) {
                Console.WriteLine ("key={0}, value={1}", kvp.Key, kvp.Value);
            }
        }
    }
}

