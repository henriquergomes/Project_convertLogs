using ConvertLog.MyCDN.Model;

namespace ConvertLog.MyCDN.IServices
{
	public interface ILogWriter
    { 

        Task WriteLogAsync(List<LogEntry> logEntries, string fullPath);
    }
}

