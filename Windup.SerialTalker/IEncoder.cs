using System;

namespace Windup.SerialTalker
{
    public interface IEncoder
    {
        byte[] Encode (string what);
    }
}

