using ConvertLog.MyCDN.Model;

namespace ConvertLog.MyCDN.IServices
{
	public interface ILogConverter
    { 
        Task<List<LogEntry>> DownloadAndConvertAsync(string url);
        Task ConvertAndWriteLogAsync(string url, string targetPath);
    }
}