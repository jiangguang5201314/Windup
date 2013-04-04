using System;

namespace Windup.SerialTalker
{
    public class MyCodecFactory : ICodecFactory
    {
        public MyCodecFactory ()
        {
        }

        public IDecoder GetDecoder ()
        {
            return new MyDecoder ();
        }

        public IEncoder GetEncoder ()
        {
            return new MyEncoder ();
        }
    }
}

