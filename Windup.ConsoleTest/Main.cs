using System;
using System.Diagnostics;
using System.Threading;
using Windup.SerialTalker;

namespace Windup.ConsoleTest
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Debug.WriteLine ("Enter Main");

            var list = SerialList.ReturnSerialList ();
            foreach (var port in list) {
                Console.WriteLine (port + "\t");
            }

            if (SerialAgent.TouchAgentPort ("COM3", 9600)) {
                var s = new SerialAgent ("COM3", 9600);
                s.AgentOpen ();
                var a = new AnalyzerNT (s);

                var f = new MyCodecFactory ();

                var codec = new Codec (f, a);
                Console.Read ();
            }
        }
    }
}
