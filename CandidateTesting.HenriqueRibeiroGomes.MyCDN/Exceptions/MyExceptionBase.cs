namespace ConvertLog.MyCDN.Exceptions
{
	public class MyExceptionBase : Exception
    {
		public MyExceptionBase() { }

        public MyExceptionBase(string message) : base(message) { }

        public MyExceptionBase(string message, Exception innerException) : base(message, innerException) { }
    }
}