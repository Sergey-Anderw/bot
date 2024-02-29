using Newtonsoft.Json;
using RestSharp;

namespace Shared.Extensions
{
	public static class RestResponseExtensions
	{
		public static bool IsServiceOk(this RestResponse response)
		{
			return response.IsSuccessful &&
			       JsonConvert.DeserializeObject<ErrorResponse>(response.Content!)?.Info == null;
		}

		public static bool IsServiceOk(this RestResponse response, out string? errorMessage)
		{
			errorMessage = null;

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content)!;

					// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
					if (errorResponse.Info != null)
					{
						errorMessage = errorResponse.Info.StackTrace;
						return false;
					}
					else
					{
						return true;
					}

				}
				catch
				{
					return true;
				}

			}

			errorMessage = $"ApplicationException.ExecutePost. {response.ErrorException!.Message}";

			return false;

		}
	}
}
