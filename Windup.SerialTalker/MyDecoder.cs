using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public class MyDecoder : IDecoder
    {
        public MyDecoder ()
        {
        }

        public Dictionary<string, string> Decode (IList<Int32> data)
        {
            var d = new Dictionary<string, string> ();
            d.Add (DateTime.Now.ToShortTimeString (), data.ToString ());
            return d;
        }
    }
}

