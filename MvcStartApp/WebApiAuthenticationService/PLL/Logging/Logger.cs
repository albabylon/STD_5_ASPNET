using System;
using System.IO;
using System.Threading;

namespace WebApiAuthenticationService.PLL.Logging
{
    public class Logger : ILogger
    {
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim(); //потокобезопасного доступ

        private string logDirectory;

        public Logger()
        {
            logDirectory = AppDomain.CurrentDomain.BaseDirectory + @"/_logs/" + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + @"/";

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
        }

        public void WriteEvent(string eventMessage)
        {
            //блокирует доступ для всех других потоков (как на чтение, так и на запись), пока текущий поток работает с файлом
            //гарантирует, что только один поток может писать в файл в данный момент
            lock_.EnterWriteLock();
            try
            {
                //режиме дописывания (append: true), чтобы новые сообщения добавлялись в конец, а не перезаписывали файл
                using StreamWriter writer = new StreamWriter(logDirectory + "events.txt", append: true);
                writer.WriteLine(eventMessage);
            }
            finally
            {
                //снимает блокировку, позволяя другим потокам снова получать доступ к файлу
                lock_.ExitWriteLock();
            }
        }

        public void WriteError(string errorMessage)
        {
            lock_.EnterWriteLock();
            try
            {
                using StreamWriter writer = new StreamWriter(logDirectory + "errors.txt", append: true);
                writer.WriteLine(errorMessage);
            }
            finally
            {
                lock_.ExitWriteLock();
            }
        }

    }
}
