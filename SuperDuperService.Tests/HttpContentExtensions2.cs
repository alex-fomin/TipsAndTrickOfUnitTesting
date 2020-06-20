using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SuperDuperService.Tests
{
	public static class HttpContentExtensions2
	{
		public static Task<T> ReadAsAnonAsync<T>(this HttpContent content, [UsedImplicitly] T anon)
		{
			return content.ReadAsAsync<T>();
		}
	}
}