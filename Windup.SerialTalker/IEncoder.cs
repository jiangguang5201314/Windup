using System;

namespace Windup.SerialTalker
{
	public interface IEncoder
	{
		void Encode (byte[] what);
	}
}

