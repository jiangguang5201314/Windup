using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
	public interface IDecoder
	{
		IList<Int32> Decode ();
	}
}

