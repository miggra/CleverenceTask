using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceTask
{
    public static class Server
    {
        private static int count = 0;
        private static ReaderWriterLockSlim serverLock = new ReaderWriterLockSlim();

        public static int GetCount()
        {
            serverLock.EnterReadLock();
            try
            {
                return count;
            }
            finally
            {
                serverLock.ExitReadLock();
            }
        }

        public static void AddToCount(int value)
        {
            serverLock.EnterWriteLock();
            try
            {
                count += 1;
            }
            finally
            {
                serverLock.ExitWriteLock();
            }
        }
    }
}
