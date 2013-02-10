using System;

namespace Windup.SerialTalker
{
	public interface ICodecFactory
	{
		IEncoder ReturnEncoder ();

		IDecoder ReturnDecoder ();

	}
}

