using ConvertLog.MyCDN.Exceptions;
using ConvertLog.MyCDN.Model;
using ConvertLog.MyCDN.Services;

namespace MyCDN.Teste;

public class LogConverterTests
{
    [Fact]
    public async Task LogConverter_DownloadAndConvertAsync_Success()
    {
        var logConverter = new LogConverter();
        var url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";

        var logEntries = await logConverter.DownloadAndConvertAsync(url);

        Assert.NotNull(logEntries);
        Assert.True(logEntries.Count > 0);
    }

    [Fact]
    public async Task LogConverter_DownloadAndConvertAsync_InvalidUrl()
    {
        var logConverter = new LogConverter();
        var url = "https://invalid-url.com/non-existent-log.txt";

        await Assert.ThrowsAsync<MyExceptionBase>(() => logConverter.DownloadAndConvertAsync(url));
    }
}