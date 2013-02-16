using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
	public class MyDecoder : IDecoder
	{
		public MyDecoder (IAnalyzer a)
		{
			a.BindListDelegate(this.TransferListDelegate);
		}

		public Dictionary<string, string> Decode (IList<Int32> data)
		{
			var d = new Dictionary<string, string> ();
			return d;
		}

		public void TransferListDelegate(List<Int32> list)
		{

		}
	}
}

