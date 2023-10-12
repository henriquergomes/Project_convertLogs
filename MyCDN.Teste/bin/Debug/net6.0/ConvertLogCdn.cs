using System;
using ConvertLog.MyCDN.Model;

namespace ConvertLog.MyCDN;

    public class ConvertLogCdn
    {

        public static async Task Main(string[] args)
        {
            DateTime currentDate = DateTime.Now;
            string dateAsString = currentDate.ToString("yyyy-MM-dd-HH-mm-ss");
            string outputFilePath = "output_" + dateAsString + ".txt";
            string directoryPath = "Output";
            string fullPath = Path.Combine(directoryPath, outputFilePath);

            string url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";

            try
            {
                var logEntries = await DownloadAndConvertAsync(url);

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
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        Console.WriteLine("Conversão concluída com sucesso. Saída gravada em. " + fullPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

        public static async Task<List<LogEntry>> DownloadAndConvertAsync(string url)
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
            {
                throw new Exception($"Falha ao baixar o arquivo. Status: {response.StatusCode}");
            }
        }
}