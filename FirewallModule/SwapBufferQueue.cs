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
        enum BufferState
        {
            DumpA,
            DumpB
        }

        private Queue<T> bufferA = new Queue<T>();
        private Queue<T> bufferB = new Queue<T>();

        private BufferState swap = BufferState.DumpB;
        private readonly object dumpLock = new object();

        public void Enqueue(T t)
        {
            lock (this)
            {
                if (swap == BufferState.DumpA)
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
                    if (swap == BufferState.DumpA)
                    {
                        swap = BufferState.DumpB;
                    }
                    else
                    {
                        swap = BufferState.DumpA;
                    }
                }
                Queue<T> ret;
                if (swap == BufferState.DumpA)
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
