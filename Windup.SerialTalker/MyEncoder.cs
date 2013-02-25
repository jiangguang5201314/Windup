using System;

namespace Windup.SerialTalker
{
    public class MyEncoder : IEncoder
    {
        public MyEncoder ()
        {
        }

        public byte[] Encode (string what)
        {
            if (string.IsNullOrEmpty (what))
                throw new ArgumentNullException ("what is null or empty!");

            byte[] b = {};
            char[] c = what.ToCharArray ();

            for (int i = 0; i < c.Length; i++) {
                b [i] = byte.Parse (c [i].ToString ());
            }

            return b;
        }
    }
}

