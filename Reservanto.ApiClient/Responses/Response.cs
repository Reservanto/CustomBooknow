using Reservanto.ApiClient.Public;

namespace  Reservanto.ApiClient.Responses
{
	public abstract class Response
	{
		public bool IsError
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string ErrorParameter
		{
			get;
			set;
		}

		public string[] ErrorMessages
		{
			get;
			set;
		}

		public string[] ErrorParameters
		{
			get;
			set;
		}
	}

	internal class Response<T> : Response where T : IResponseResult
	{
		public T Result
		{
			get;
			set;
		}
	}
}
