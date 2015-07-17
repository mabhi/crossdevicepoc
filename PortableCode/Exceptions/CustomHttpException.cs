using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace PortableCode.Exceptions
{
	[Serializable()]
	public class CustomHttpException : HttpRequestException
	{

		public CustomHttpException()
				: base() { }

			public CustomHttpException(string message)
				: base(message) { }

			public CustomHttpException(string format, params object[] args)
				: base(string.Format(format, args)) { }

			public CustomHttpException(string message, Exception innerException)
				: base(message, innerException) { }

			public CustomHttpException(string format, Exception innerException, params object[] args)
				: base(string.Format(format, args), innerException) { }

			protected CustomHttpException(SerializationInfo info, StreamingContext context)
				: base(info, context) { }
		
	}
}

