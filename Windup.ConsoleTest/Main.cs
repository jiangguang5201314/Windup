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
            Type t = Type.GetType ("Mono.Runtime");
            var r = t != null ? "Mono" : ".NET";
            Console.WriteLine ("WindupConsoleTest v1.0.0");
            Console.WriteLine ("OS: " + Environment.OSVersion.Platform);
            Console.WriteLine ("Runtime: " + r + Environment.Version.Major);
            /*
            var list = SerialList.ReturnSerialList ();
            foreach (var port in list) {
                Console.WriteLine (port + "\t");
            }
*/

            if (SerialAgent.TouchAgentPort ("COM3", 9600)) {
                SerialAgent s;
                AnalyzerNT a;
                MyCodecFactory f;
                Codec codec;

                s = new SerialAgent ("COM3", 9600);
                s.AgentOpen ();
                a = new AnalyzerNT (s);
                f = new MyCodecFactory ();
                codec = new Codec (f, a);

                while (true) {
                    Console.Write (">>>");
                    var cmd = Console.ReadLine ();
                    if ("EXIT" == cmd.ToUpper ()) {
                        break;
                    } else {
                        codec.WriteData (cmd);
                    }
                }
                s.AgentClose ();
            } else {
                Console.WriteLine ("Serial ports may be occupied.");
                Console.WriteLine ("Press Enter exit.");
                Console.Read ();
            }
        }
    }
}
