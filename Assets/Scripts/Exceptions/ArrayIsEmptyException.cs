using System;

namespace Archero.Exceptions
{

    [Serializable]
	public class ArrayIsEmptyException : Exception
	{
		public ArrayIsEmptyException() { }
		public ArrayIsEmptyException(string message) : base(message) { }
		public ArrayIsEmptyException(string message, Exception inner) : base(message, inner) { }
		protected ArrayIsEmptyException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
