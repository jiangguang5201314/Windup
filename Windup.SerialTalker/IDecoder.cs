using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
	public interface IDecoder
	{
		Dictionary<string, string> Decode (IList<Int32> data);
	}
}

