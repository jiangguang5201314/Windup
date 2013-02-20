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
            var tempString = "";
            foreach (Int32 dt in data) {
                tempString += dt.ToString ();
            }
            d.Add (DateTime.Now.ToLongTimeString (), tempString);
            return d;
        }
    }
}

