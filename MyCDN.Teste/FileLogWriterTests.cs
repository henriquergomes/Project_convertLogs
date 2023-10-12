using System;
using ConvertLog.MyCDN.Model;
using ConvertLog.MyCDN.Services;

namespace MyCDN.Teste
{
	public class FileLogWriterTests
	{
        [Fact]
        public async Task FileLogWriter_WriteLogAsync_Success()
        {
            var logWriter = new FileLogWriter();
            var logEntries = new List<LogEntry>
            {
                new LogEntry
                {
                    ProviderId = 1,
                    HttpStatusCode = 200,
                    CacheStatus = "Hit",
                    HttpMethod = "GET",
                    UriPath = "/test",
                    TimeTaken = 0.5
                }
            };

            var outputPath = "test-output.txt";

            await logWriter.WriteLogAsync(logEntries, outputPath);

            Assert.True(File.Exists(outputPath));

            File.Delete(outputPath);
        }
    }
}

