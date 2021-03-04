using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Helper;
using Xamarin.Forms;

namespace TimeSheetDriversApp.Mobile.Helper
{
    public class APIService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static Model.DTO.UserDTO CurrentUser { get; set; }

        private readonly string _route;

#if DEBUG
        private string _apiUrl = "http://10.0.2.2:56796/api";
#endif
#if RELEASE
        private string _apiUrl = "http://10.0.2.2:56796/api";
#endif

        public APIService(string route)
        {
            _route = route;
        }
        public async Task<T> Get<T>(object search, string actionName = null)
        {
            var url = $"{_apiUrl}/{_route}";
            if (actionName != null)
            {
                url += "/";
                url += actionName;
            }

            try
            {
                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authenticated.", "OK");
                }
                throw;
            }
        }

        public async Task<T> GetById<T>(object id, string actionName = null)
        {
            var url = $"{_apiUrl}/{_route}";
            if (actionName != null)
                url += "/" + actionName;
            url += "/" + id;

            try
            {
                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authenticated.", "OK");
                }
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<T> Insert<T>(object request, string actionName = null)
        {
            var url = $"{_apiUrl}/{_route}";
            if (actionName != null)
            {
                url += "/";
                url += actionName;

            }

            try
            {
                return await url.WithBasicAuth(Username, Password).PostJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                await Application.Current.MainPage.DisplayAlert("Greška", stringBuilder.ToString(), "OK");

                return default(T);
            }

        }

        public async Task<T> Update<T>(int id, object request)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";

                return await url.WithBasicAuth(Username, Password).PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }
                await Application.Current.MainPage.DisplayAlert("Greška", stringBuilder.ToString(), "OK");
                return default(T);
            }

        }
    }
}
