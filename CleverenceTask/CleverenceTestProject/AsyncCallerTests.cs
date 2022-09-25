using CleverenceTask2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceTestProject
{
    public class AsyncCallerTests
    {
        [Theory]
        [InlineData(1000)]
        public void AsyncCallerWaitWhenWorkTimeIsSmall(int executionTime)
        {
            var execution = (object sender, EventArgs e) => Thread.Sleep(executionTime);
            EventHandler h = new EventHandler(execution);
            AsyncCaller ac = new AsyncCaller(h);
            bool completedOK = ac.Invoke(5000, null, EventArgs.Empty);
            Assert.True(completedOK);
        }

        [Theory]
        [InlineData(6000)]
        public void AsyncCallerHangUpOnTimeout(int executionTime)
        {
            var execution = (object sender, EventArgs e) => Thread.Sleep(executionTime);
            EventHandler h = new EventHandler(execution);
            AsyncCaller ac = new AsyncCaller(h);
            bool completedOK = ac.Invoke(5000, null, EventArgs.Empty);
            Assert.False(completedOK);
        }
    }
}
