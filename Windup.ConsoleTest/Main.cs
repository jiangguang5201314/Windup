using System;
using System.Diagnostics;
using Windup.SerialTalker;

namespace Windup.ConsoleTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Debug.WriteLine("Enter Main");
			if (SerialAgent.TouchAgentPort ("COM3", 9600)) {
				var s = new SerialAgent ("COM3", 9600);
				s.AgentOpen();
				byte[] b = new byte[]{12, 34, 56};
				for(int i = 0; i < 100; i++){
					Debug.WriteLine("write");
					s.AgentWrite(b);
				}
			}
		}
	}
}
