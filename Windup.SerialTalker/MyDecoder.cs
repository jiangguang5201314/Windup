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
            var result = new Dictionary<string, string> ();
            var tempString = "|";
            foreach (var d in data) {
                tempString += d.ToString () + "|";
            }
            result.Add (DateTime.Now.ToLongTimeString (), tempString);
            return result;
        }
    }
}

