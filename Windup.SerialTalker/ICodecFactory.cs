using System;

namespace Windup.SerialTalker
{
	public interface ICodecFactory
	{
		IEncoder GetEncoder ();

		IDecoder GetDecoder ();

	}
}

