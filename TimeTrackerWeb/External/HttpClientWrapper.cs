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
        Task<List<TEntity>> GetAllAsync(string urlExtension);
        Task<TEntity> GetById(string urlExtension, string id);
        Task<bool> PutAsync(string urlExtension, string id, TEntity model);
        Task<bool> PostAsync(string urlExtension, TEntity model);
        Task<bool> DeleteAsync(string urlExtension, string id);
    }

    public class HttpClientWrapper<TEntity> : IHttpClientWrapper<TEntity>
    {
        private IHttwrapClient _httwrap;

        public HttpClientWrapper(IConfiguration configuration)
        {
            CheckExceptions(async () => SetupHttpConnection(configuration));
        }

        public async Task<List<TEntity>> GetAllAsync(string urlExtension)
        {
            return await CheckExceptions(async () => (await _httwrap.GetAsync(urlExtension)).ReadAs<List<TEntity>>());
        }

        public async Task<TEntity> GetById(string urlExtension, string id)
        {
            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + id)).ReadAs<TEntity>());
        }

        public async Task<bool> PutAsync(string urlExtension, string id, TEntity model)
        {
            return await CheckExceptions(async () => (await _httwrap.PutAsync(urlExtension + "/" + id, model)).Success);
        }

        public async Task<bool> PostAsync(string urlExtension, TEntity model)
        {
            return await CheckExceptions(async () => (await _httwrap.PostAsync(urlExtension, model)).Success);
        }

        public async Task<bool> DeleteAsync(string urlExtension, string id)
        {
            return await CheckExceptions(async () => (await _httwrap.DeleteAsync(urlExtension + "/" + id)).Success);
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
                throw new HttpRequestException("The Datasource Link Has Changed", httpwrapException);
            }
        }
    }
}