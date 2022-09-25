using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceTask2
{
    public class AsyncCaller
    {
        private readonly EventHandler handler;
        private Thread thread;

        public AsyncCaller(EventHandler handler)
        {
            this.handler = handler;
        }

        public bool Invoke(int timeout, object sender, EventArgs e)
        {
            Task handlerTask =  Task.Run(() => handler.Invoke(sender, e));
            Task timeoutTask = Task.Delay(timeout);
            Task completedTask = Task.WhenAny(handlerTask, timeoutTask).Result;
            return handlerTask.IsCompleted;
        }
    }
}
