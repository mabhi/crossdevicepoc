using System;
using System.Runtime.Serialization;

namespace PortableCode.Exceptions
{
//	[DataContract]
	public class CustomHttpException : Exception
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

//			protected CustomHttpException(SerializationInfo info, StreamingContext context)	: base(info, context) { }
		
	}
}

