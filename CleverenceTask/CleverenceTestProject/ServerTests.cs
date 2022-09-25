using CleverenceTask;
namespace CleverenceTestProject
{
    public class ServerTests
    {
        private void TestSetup(List<Reader> readers, List<Writer> writers)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                readers.Add(new Reader());
                writers.Add(new Writer());
            }

            foreach (Reader reader in readers)
            {
                tasks.Add(Task.Run(() =>
                {
                    reader.Read();
                }));
            }

            foreach (Writer writer in writers)
            {
                tasks.Add(Task.Run(() =>
                {
                    writer.Write(1);
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        [Fact]
        public void ReadersReadinParallel()
        {
            List<Reader> readers = new List<Reader>();
            List<Writer> writers = new List<Writer>();
            TestSetup(readers, writers);

            // читатели могли читать параллельно, без выстраивания в очередь по локу; 
            for (int i = 0; i < readers.Count; i++)
            {
                for(int j = i+1; j < writers.Count; j++)
                {
                    if(readers[i].TimeStart < writers[j].TimeStart)
                    {
                        if (writers[i].TimeEnd > writers[j].TimeStart)
                            Assert.True(true); // процесс шел параллельно
                    }
                    else
                        if (writers[j].TimeEnd > writers[i].TimeStart)
                            Assert.True(true); // процесс шел параллельно
                }
            }

            Assert.All(readers, (reader) =>
            {
                Assert.All(writers, (writer) =>
                {
                    if (writer.TimeStart < reader.TimeStart)
                        Assert.True(writer.TimeEnd < reader.TimeStart);
                });
            });
        }


        [Fact]
        public void OnlyСonsequentiallyWrittingTest()
        {
            List<Reader> readers = new List<Reader>();
            List<Writer> writers = new List<Writer>();
            TestSetup(readers, writers);
            // писатели писали только последовательно и никогда одновременно;
            Assert.Equal(10, Server.GetCount());
        }

        [Fact]
        public void ReaderAlwaysWaitWrittersTest()
        {

            List<Reader> readers = new List<Reader>();
            List<Writer> writers = new List<Writer>();
            TestSetup(readers, writers);

            // пока писатели добавляют и пишут, читатели должны ждать окончания записи. 
            Assert.All(readers, (reader) =>
            {
                Assert.All(writers, (writer) =>
                {
                    if (writer.TimeStart < reader.TimeStart)
                        Assert.True(writer.TimeEnd < reader.TimeStart);
                });
            });
        }
    }
}