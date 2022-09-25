using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceTask
{
    public class Reader
    {
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public void Read()
        {
            TimeStart = DateTime.Now;
            Server.GetCount();
            TimeEnd = DateTime.Now;
        }
    }
}
