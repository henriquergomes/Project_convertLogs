using ConvertLog.MyCDN.Exceptions;
using ConvertLog.MyCDN.IServices;
using ConvertLog.MyCDN.Model;

namespace ConvertLog.MyCDN.Services
{
    public class LogConverter : ILogConverter
    {
        public async Task<List<LogEntry>> DownloadAndConvertAsync(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var logEntries = lines.Select(line =>
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        return new LogEntry
                        {
                            ProviderId = int.Parse(parts[0]),
                            HttpStatusCode = int.Parse(parts[1]),
                            CacheStatus = parts[2],
                            HttpMethod = parts[3].Split(' ')[0].Trim('"'),
                            UriPath = parts[3].Split(' ')[1],
                            TimeTaken = double.Parse(parts[4])
                        };
                    }
                    return null;
                }).Where(entry => entry != null).ToList();

                return logEntries;
            }
            else
                throw new MyExceptionBase($"Failed to download the file. Status: {response.StatusCode}");
            
        }

        public async Task ConvertAndWriteLogAsync(string url, string targetPath)
        {
            var logEntries = await DownloadAndConvertAsync(url);

            ILogWriter logWriter = new FileLogWriter();
            await logWriter.WriteLogAsync(logEntries, targetPath);
            Console.WriteLine("Conversion completed successfully. Output recorded in. " + targetPath);
        }
    }
}