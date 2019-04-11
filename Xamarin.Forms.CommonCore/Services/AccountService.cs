using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLCrypto;
using Plugin.Settings;
using Xamarin.Auth;
using Xamarin.Forms.CommonCore;
using static PCLCrypto.WinRTCrypto;

#if __ANDROID__
using Plugin.CurrentActivity;
#endif

namespace Xamarin.Forms.CommonCore
{
    public class AccountService : IAccountService
    {
        const string protectedStore = "protectedStoreTokenAccount";
        const string pwKey = "password";

        public string AccountEncryptionKey
        {
            get
            {
                return CrossSettings.Current.GetValueOrDefault("AccountEncryptionKey", Guid.NewGuid().ToString());
			}
			set { CrossSettings.Current.AddOrUpdateValue("AccountEncryptionKey", value); }
		}

        public IEncryptionService Encryption
        {
            get { return CoreDependencyService.GetService<IEncryptionService, EncryptionService>(true); }
        }

		public async Task<(bool Success, Exception Error)> SaveAccountStore<T>(string username, T obj) where T : class, new()
		{
            return await Task.Run(() =>
            {
                Exception excep = null;
                try
                {
                    var account = GetAccount(username);
                    PersistAccount(account, username, null, obj);
                    SaveAccount(account, username);
                }
                catch (Exception ex)
                {
                    excep = ex;
                }
                return (excep == null ? true : false, excep);
            });

		}
		public async Task<(T Response, bool Success, Exception Error)> GetAccountStore<T>(string username) where T : class, new()
		{
            return await Task.Run(() =>
            {
                T response = null;
                Exception excep = null;
                try
                {
                    var account = GetAccount(username);
                    response = LoadAccount<T>(account, username);
                }
                catch (Exception ex)
                {
                    excep = ex;
                }
                return (response, excep == null ? true : false, excep);

            });

		}
		public async Task<(bool Success, Exception Error)> SaveAccountStore<T>(string username, string password, T obj, bool pwdHashed=false) where T : class, new()
		{
            return await Task.Run(() =>
            {
                Exception excep = null;
                try
                {
                    var account = GetAccount(username);
                    PersistAccount(account, username, password, obj, pwdHashed);
                    SaveAccount(account, username);
  
                }
                catch (Exception ex)
                {
                    excep = ex;
                }
                return (excep == null ? true : false, excep);
            });

		}
        public async Task<(T Response, bool Success, Exception Error)> GetAccountStore<T>(string username, string password, bool pwdHashed = false) where T : class, new()
		{
            return await Task.Run(() =>
            {
                Exception excep = null;
                T response = null;
                try
                {
                    var account = GetAccount(username);
                    response = LoadAccount<T>(account, password, pwdHashed);
                }
                catch (Exception ex)
                {
                    excep = ex;
                }
                return (response, excep == null ? true : false, excep);
            });

		}

        private void PersistAccount<T>(Account account, string username, string password, T obj, bool pwdHashed = false) where T : class, new()
		{
			var data = JsonConvert.SerializeObject(obj);
            var hash = pwdHashed ? password : Encryption.GetHashString(password);
			if (account.Properties.ContainsKey(typeof(T).Name))
			{
				if (!string.IsNullOrEmpty(password))
				{
                    
                    account.Properties[pwKey] = hash;
                    account.Properties[typeof(T).Name] = Encryption.AesEncrypt(data, hash);
				}
				else
				{
					account.Properties[typeof(T).Name] = data;
				}

			}
			else
			{
				if (!string.IsNullOrEmpty(password))
				{
                    account.Properties.Add(pwKey, hash);
                    account.Properties.Add(typeof(T).Name, Encryption.AesEncrypt(data, hash));
				}
				else
				{
					account.Properties.Add(typeof(T).Name, data);
				}

			}
		}
        private T LoadAccount<T>(Account account, string password, bool pwdHashed = false) where T : class, new()
		{
			if (!string.IsNullOrEmpty(password))
			{
                var hashedPassword = pwdHashed ? password : Encryption.GetHashString(password);
				if (account.Properties.ContainsKey(typeof(T).Name) && account.Properties[pwKey] == hashedPassword)
				{
                    var data = Encryption.AesDecrypt(account.Properties[typeof(T).Name], hashedPassword);
					return JsonConvert.DeserializeObject<T>(data);
				}

			}
			else
			{
				var data = account.Properties[typeof(T).Name];
				return JsonConvert.DeserializeObject<T>(data);
			}
			return null;

		}
       
		private AccountStore GetStore()
		{
#if __ANDROID__
            return AccountStore.Create(CrossCurrentActivity.Current.Activity);
#else
			return AccountStore.Create();
#endif
		}
		private void SaveAccount(Account account, string username)
		{
			if (username == null)
				return;

			var store = GetStore();
			store.Save(account, protectedStore);
		}
		private Account GetAccount(string username)
		{
			if (username == null)
				return null;

			var store = GetStore();
			var accounts = store.FindAccountsForService(protectedStore);
			var storeAccount = accounts.FirstOrDefault(a => a.Username == username);
			if (storeAccount == null)
			{
				storeAccount = new Account(username);
			}
			return storeAccount;
		}

    }
}

