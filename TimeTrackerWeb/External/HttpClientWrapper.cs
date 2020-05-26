using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using Httwrap;
using Httwrap.Interface;
using Microsoft.Extensions.Configuration;

namespace TimeTrackerWeb.External
{
    public interface IHttpClientWrapper<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(string urlExtension, string token = null);
        Task<List<TEntity>> GetAllByIdAsync(string urlExtension, string itemId, string token = null);
        Task<TEntity> GetByIdAsync(string urlExtension, string id, string token = null);
        Task<bool> PutAsync(string urlExtension, string id, TEntity model, string token = null);
        Task<bool> PostAsync(string urlExtension, TEntity model, string token = null);
        Task<string> PostWithResultAsync(string urlExtension, TEntity model, string token = null);
        Task<bool> DeleteAsync(string urlExtension, string id, string token = null);
    }

    public class HttpClientWrapper<TEntity> : IHttpClientWrapper<TEntity>
    {
        private IHttwrapClient _httwrap;

        public HttpClientWrapper(IConfiguration configuration)
        {
            CheckExceptions(async () => SetupHttpConnection(configuration));
        }

        public async Task<List<TEntity>> GetAllAsync(string urlExtension, string token = null)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<List<TEntity>>());
        }

        public async Task<List<TEntity>> GetAllByIdAsync(string urlExtension, string itemId, string token = null)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + itemId, null,
                    token != null ? GetCustomHeaders(token) : null))
                .ReadAs<List<TEntity>>());
        }

        public async Task<TEntity> GetByIdAsync(string urlExtension, string id, string token = null)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + id, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<TEntity>());
        }

        public async Task<bool> PutAsync(string urlExtension, string id, TEntity model, string token = null)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.PutAsync(urlExtension + "/" + id, model, null,
                    token != null ? GetCustomHeaders(token) : null)).Success);
        }

        public async Task<bool> PostAsync(string urlExtension, TEntity model, string token = null)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.PostAsync(urlExtension, model, null, token != null ? GetCustomHeaders(token) : null))
                .Success);
        }

        public async Task<string> PostWithResultAsync(string urlExtension, TEntity model, string token = null)
        {
            return (await _httwrap.PostAsync(urlExtension, model, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<string>();
        }

        public async Task<bool> DeleteAsync(string urlExtension, string id, string token = null)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.DeleteAsync(urlExtension + "/" + id, null,
                    token != null ? GetCustomHeaders(token) : null)).Success);
        }

        private void SetupHttpConnection(IConfiguration configuration)
        {
            var usersApiUrl = configuration["ConnectionStrings:TimeTrackerApi"];

            IHttwrapConfiguration httwrapConfiguration = new HttwrapConfiguration(usersApiUrl);
            _httwrap = new HttwrapClient(httwrapConfiguration);
        }

        private T CheckExceptions<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (SocketException socketException)
            {
                throw new HttpRequestException("Could Not Connect To Data Source", socketException);
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new HttpRequestException("Could Not Connect To Data Source", httpRequestException);
            }
            catch (HttwrapException httpwrapException)
            {
                throw new HttpRequestException("The Data Source Link Has Changed", httpwrapException);
            }
        }

        private Dictionary<string, string> GetCustomHeaders(string token)
        {
            return new Dictionary<string, string> {{"authorization", "Bearer " + token}};
        }
    }
}