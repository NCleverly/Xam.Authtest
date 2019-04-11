using System;
using System.Threading.Tasks;

namespace Xamarin.Forms.CommonCore
{
	public interface IAccountService
	{
		Task<(bool Success,Exception Error)> SaveAccountStore<T>(string username, T obj) where T : class, new();
        Task<(T Response, bool Success, Exception Error)> GetAccountStore<T>(string username) where T : class, new();
        Task<(bool Success, Exception Error)> SaveAccountStore<T>(string username, string password, T obj, bool pwdHashed = false) where T : class, new();
        Task<(T Response, bool Success, Exception Error)> GetAccountStore<T>(string username, string password, bool pwdHashed = false) where T : class, new();
	}
}
