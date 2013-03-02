using System;
using System.Text;

namespace Windup.SerialTalker
{
    public class MyEncoder : IEncoder
    {
        public MyEncoder ()
        {
        }

        public byte[] Encode (string what)
        {
            //if (string.IsNullOrEmpty (what))
            //throw new ArgumentNullException ("what is null or empty!");
            //what = "0";
            return Encoding.Default.GetBytes (what);
        }
    }
}

