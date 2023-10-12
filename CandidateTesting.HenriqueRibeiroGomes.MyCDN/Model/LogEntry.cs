namespace ConvertLog.MyCDN.Model
{
	public class LogEntry
	{
        public int ProviderId { get; set; }
        public int HttpStatusCode { get; set; }
        public string CacheStatus { get; set; }
        public string HttpMethod { get; set; }
        public string UriPath { get; set; }
        public double TimeTaken { get; set; }

        public override string ToString()
        {
            return $"\"My CDN\" {HttpMethod} {HttpStatusCode} {UriPath} {TimeTaken} {ProviderId} {CacheStatus}";
        }
    }
}