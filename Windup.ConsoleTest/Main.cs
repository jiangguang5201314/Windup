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
                Console.WriteLine (port);
            }

            /*
            if (SerialAgent.TouchAgentPort ("COM3", 9600)) {
                var s = new SerialAgent ("COM3", 9600);
                s.AgentOpen ();
                for (int i = 0; i < 100; i++) {
                    byte[] b = new byte[]{12, 34, 56};
                    var result = s.AgentWrite (b);
                    Console.WriteLine (result);
                }
                Console.ReadKey ();
                s.AgentClose ();
                Debug.WriteLine ("close agent");
            }
*/

            if (SerialAgent.TouchAgentPort ("COM3", 9600)) {
                var s = new SerialAgent ("COM3", 9600);
                s.AgentOpen ();
                var a = new AnalyzerNT (s);

                var f = new MyCodecFactory ();

                var codec = new Codec (f, a);
                Thread.Sleep (100000);
            }
        }
    }
}
