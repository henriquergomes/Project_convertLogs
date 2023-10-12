using System;
using ConvertLog.MyCDN.IServices;
using ConvertLog.MyCDN.Services;

namespace ConvertLog.MyCDN
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                ILogConverter logConverter = new LogConverter();
                ILogWriter logWriter = new FileLogWriter();

                await logConverter.ConvertAndWriteLogAsync(logWriter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}