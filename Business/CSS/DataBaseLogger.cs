using System;
using System.Collections.Generic;

namespace Business.CCS
{
    public class DataBaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veri tabanına loglandı");
        }
    }
}