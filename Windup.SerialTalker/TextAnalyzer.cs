using System;
using System.Collections.Generic;

namespace Windup.SerialTalker
{
    public class TextAnalyzer : IAnalyzer
    {
        Queue<Int32> queue;
        object lock_obj = new object ();
        public TextAnalyzer ()
        {
            queue = new Queue<Int32> ();
        }

        public void GetDataFromSerialAgent (Int32 data)
        {
            lock (lock_obj) {
                queue.Enqueue (data);
            }
        }
    }
}

