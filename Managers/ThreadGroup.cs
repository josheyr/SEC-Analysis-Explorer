using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace SEC_Analysis.Managers
{
    class ThreadGroup
    {
        /// <summary>
        /// List of tasks to be assigned to threads
        /// </summary>
        volatile Collection<Func<int>> taskList = new Collection<Func<int>>();

        /// <summary>
        /// list of threads to be assigned tasks
        /// </summary>
        Collection<Thread> threads = new Collection<Thread>();

        volatile int threadCount = 0;
        volatile int taskListSize = 0;

        /// <summary>
        /// Constructs threadgroup class
        /// </summary>
        /// <param name="threadCount">amount of threads</param>
        public ThreadGroup(int threadCount = 20)
        {
            this.threadCount = threadCount;
        }


        public void threadProcess()
        {
            while (taskList.Count > 0)
            {
                
                Func<int> method_new = taskList[0];
                taskList.RemoveAt(0);
                method_new();
                taskListSize--;

                Console.WriteLine(threadCount);

            }
            threadCount--;

            Console.WriteLine(threadCount);
        }


        /// <summary>
        /// Begins task and assigns it to a thread.
        /// </summary>
        /// <param name="method"></param>
        public void startTask(Func<int> method)
        {
            taskList.Add(method);
            if (threadCount < threads.Count)
            {
                Thread thread = new Thread(threadProcess);
                threads.Add(thread);

                thread.Start();
            }
            else
            {

            }

            taskListSize++;
        }



        /// <summary>
        /// Has all threads finished their tasks? We do this by having a task counter that increments once a task begins and decrements once a task ends
        /// </summary>
        /// <returns></returns>
        public bool isAllThreadsFinished()
        {
            int threadCounter = threadCount;

            return (threadCounter == 0);
        }

        /// <summary>
        /// Joins all the threads
        /// </summary>
        public void joinAllThreads()
        {
            foreach(Thread thread in threads)
            {
                thread.Join();
            }

            threads.Clear();
        }
    }
}
