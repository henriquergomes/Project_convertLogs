using ConvertLog.MyCDN.Exceptions;
using ConvertLog.MyCDN.IServices;
using ConvertLog.MyCDN.Model;

namespace ConvertLog.MyCDN.Services
{
    public class FileLogWriter : ILogWriter
    {
        public async Task WriteLogAsync(List<LogEntry> logEntries, string fullPath)
        {
            using (var writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("#Version: 1.0");
                writer.WriteLine($"#Date: {DateTime.Now:dd/MM/yyyy HH:mm}");
                writer.WriteLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");

                foreach (var entry in logEntries)
                {
                    writer.WriteLine(entry.ToString());
                }

                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(fullPath))) Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                }
                catch (MyExceptionBase ex)
                {
                    Console.WriteLine($"Corrected an error: {ex.Message}");
                }
            }
        }
    }
}

