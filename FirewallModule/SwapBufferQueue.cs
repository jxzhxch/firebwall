using System;
using System.Collections.Generic;
using System.Text;

namespace FM
{
    /// <summary>
    /// The point of this is to make a queue that can take input from multiple threads at the same time while a single thread is dumping the information in the queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SwapBufferQueue<T>
    {
        private Queue<T> bufferA = new Queue<T>();
        private Queue<T> bufferB = new Queue<T>();

        //if false, A is being filled
        private bool swap = false;
        private readonly object dumpLock = new object();

        public void Enqueue(T t)
        {
            lock (this)
            {
                if (swap)
                {
                    bufferB.Enqueue(t);
                }
                else
                {
                    bufferA.Enqueue(t);
                }
            }
        }

        public Queue<T> DumpBuffer()
        {
            lock (dumpLock)
            {
                lock (this)
                {
                    swap = !swap;
                }
                Queue<T> ret;
                if (swap)
                {
                    ret = new Queue<T>(bufferA);
                    bufferA.Clear();
                }
                else
                {
                    ret = new Queue<T>(bufferB);
                    bufferB.Clear();
                }
                return ret;
            }
        }
    }
}
