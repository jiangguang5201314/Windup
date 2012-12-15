using System;
using NUnit.Framework;
using Windup.SerialTalker;

namespace Windup.SerialTalkerTest
{
	[TestFixture]
	public class SerialAgentTest
	{
		public SerialAgentTest ()
		{
		}

		[Test]
		public void SerialAgentConstructTest()
		{
			SerialAgent s = null;
			s = new SerialAgent();
			Assert.IsNotNull(s);
			s = null;

			s = new SerialAgent("COM3");
			Assert.IsNotNull(s);
			Assert.AreEqual(s.AgentPortName, "COM3");
			s = null;

			s = new SerialAgent("COM3", 9600);
			Assert.IsNotNull(s);
			Assert.AreEqual(s.AgentPortName, "COM3");
			Assert.AreEqual(s.AgentBaudRate, 9600);
		}

		[Test]
		public void TouchAgentPortTest()
		{
			var result = SerialAgent.TouchAgentPort("COM3", 9600);
			Assert.AreEqual(result, true);
		}
	}
}

