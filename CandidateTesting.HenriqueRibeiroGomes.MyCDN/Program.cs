using ConvertLog.MyCDN.Exceptions;
using ConvertLog.MyCDN.IServices;
using ConvertLog.MyCDN.Services;

namespace ConvertLog.MyCDN
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string url = "";
            string targetPath = "";
            DateTime currentDate = DateTime.Now;
            string dateAsString = currentDate.ToString("yyyy-MM-dd-HH-mm-ss");
            string outputFilePath = "output_" + dateAsString + ".txt";
            string directoryPath = "Output";

            if (args.Length == 0)
            {
                url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
                targetPath = Path.Combine(directoryPath, outputFilePath);
            }
            else
            {
                url = args[0] != null || args[0] != "" ?
                    args[0] : "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";

                string pathString = args[1];
                string[] partsParh = pathString.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                targetPath = Path.Combine(partsParh[0], partsParh[1]);
            }
            
            try
            {
                ILogConverter logConverter = new LogConverter();
                await logConverter.ConvertAndWriteLogAsync(url, targetPath);
            }
            catch (MyExceptionBase ex)
            {
                Console.WriteLine($"Corrected an error: {ex.Message}");
            }
        }
    }
}