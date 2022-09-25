using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceTask
{
    public class Writer
    {
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public void Write(int items)
        {
            TimeStart = DateTime.Now;
            Server.AddToCount(items);
            TimeEnd = DateTime.Now;
        }
    }
}
