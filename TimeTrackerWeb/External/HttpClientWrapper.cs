using Httwrap;
using Httwrap.Interface;
using Microsoft.Extensions.Configuration;
using Sartain_Studios_Common.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TimeTrackerWeb.External
{
    public interface IHttpClientWrapper<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(string urlExtension, string token = null);
        Task<List<TEntity>> GetAllByIdAsync(string urlExtension, string itemId, string token = null);
        Task<List<T>> GetAllByIdAsync<T>(string urlExtension, string itemId, string token = null);
        Task<TEntity> GetByIdAsync(string urlExtension, string id, string token = null);
        Task<T> GetByIdAsync<T>(string urlExtension, string id, string token = null);
        Task<bool> PutAsync(string urlExtension, string id, TEntity model, string token = null);
        Task<bool> PostAsync(string urlExtension, TEntity model, string token = null);
        Task<string> PostWithResultAsync(string urlExtension, TEntity model, string token = null);
        Task<bool> DeleteAsync(string urlExtension, string id, string token = null);
    }

    public class HttpClientWrapper<TEntity> : IHttpClientWrapper<TEntity>
    {
        private IHttwrapClient _httwrap;
        private ILoggerWrapper _loggerWrapper;

        public HttpClientWrapper(IConfiguration configuration, ILoggerWrapper loggerWrapper)
        {
            _loggerWrapper = loggerWrapper;

            CheckExceptions(async () => SetupHttpConnection(configuration));
        }

        public async Task<List<TEntity>> GetAllAsync(string urlExtension, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(GetAllAsync) + "()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<List<TEntity>>());
        }

        public async Task<List<TEntity>> GetAllByIdAsync(string urlExtension, string itemId, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(itemId) + ": " + itemId + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(GetAllByIdAsync) + "()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + itemId, null,
                    token != null ? GetCustomHeaders(token) : null))
                .ReadAs<List<TEntity>>());
        }

        public async Task<List<T>> GetAllByIdAsync<T>(string urlExtension, string itemId, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(itemId) + ": " + itemId + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(GetAllByIdAsync) + "<T>()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + itemId, null,
                    token != null ? GetCustomHeaders(token) : null))
                .ReadAs<List<T>>());
        }

        public async Task<TEntity> GetByIdAsync(string urlExtension, string id, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(id) + ": " + id + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(GetByIdAsync) + "()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + id, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<TEntity>());
        }

        public async Task<T> GetByIdAsync<T>(string urlExtension, string id, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(id) + ": " + id + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(GetByIdAsync) + "<T>()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.GetAsync(urlExtension + "/" + id, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<T>());
        }

        public async Task<bool> PutAsync(string urlExtension, string id, TEntity model, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(id) + ": " + id + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(PutAsync) + "()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.PutAsync(urlExtension + "/" + id, model, null,
                    token != null ? GetCustomHeaders(token) : null)).Success);
        }

        public async Task<bool> PostAsync(string urlExtension, TEntity model, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(model) + ": " + "cannot display" + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(PostAsync) + "()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.PostAsync(urlExtension, model, null, token != null ? GetCustomHeaders(token) : null))
                .Success);
        }

        public async Task<string> PostWithResultAsync(string urlExtension, TEntity model, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(model) + ": " + "cannot display" + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(PostWithResultAsync) + "()", null);

            return (await _httwrap.PostAsync(urlExtension, model, null, token != null ? GetCustomHeaders(token) : null))
                .ReadAs<string>();
        }

        public async Task<bool> DeleteAsync(string urlExtension, string id, string token = null)
        {
            _loggerWrapper.LogInformation(nameof(urlExtension) + ": " + urlExtension + " | " + nameof(id) + ": " + id + " | " + nameof(token) + ": " + token, this.GetType().Name, nameof(DeleteAsync) + "()", null);

            return await CheckExceptions(async () =>
                (await _httwrap.DeleteAsync(urlExtension + "/" + id, null,
                    token != null ? GetCustomHeaders(token) : null)).Success);
        }

        private void SetupHttpConnection(IConfiguration configuration)
        {
            var timeTrackerApi = configuration["ConnectionStrings:TimeTrackerApi"];

            _loggerWrapper.LogInformation("Setting up http connection with usersApiUrl of " + timeTrackerApi, this.GetType().Name, nameof(SetupHttpConnection) + "()", null);

            IHttwrapConfiguration httwrapConfiguration = new HttwrapConfiguration(timeTrackerApi);
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
                _loggerWrapper.LogError(nameof(SocketException) + "Could Not Connect To Data Source", this.GetType().Name, nameof(CheckExceptions) + "()", null);
                _loggerWrapper.LogError(nameof(SocketException) + socketException.Message, this.GetType().Name, nameof(CheckExceptions) + "()", null);

                throw new HttpRequestException("Could Not Connect To Data Source", socketException);
            }
            catch (HttpRequestException httpRequestException)
            {
                _loggerWrapper.LogError(nameof(HttpRequestException) + "Could Not Connect To Data Source", this.GetType().Name, nameof(CheckExceptions) + "()", null);
                _loggerWrapper.LogError(nameof(HttpRequestException) + httpRequestException.Message, this.GetType().Name, nameof(CheckExceptions) + "()", null);

                throw new HttpRequestException("Could Not Connect To Data Source", httpRequestException);
            }
            catch (HttwrapException httpwrapException)
            {
                _loggerWrapper.LogError(nameof(HttwrapException) + "The Data Source Link Has Changed", this.GetType().Name, nameof(CheckExceptions) + "()", null);
                _loggerWrapper.LogError(nameof(HttwrapException) + httpwrapException.Message, this.GetType().Name, nameof(CheckExceptions) + "()", null);

                throw new HttpRequestException("The Data Source Link Has Changed", httpwrapException);
            }
        }

        private Dictionary<string, string> GetCustomHeaders(string token)
        {
            _loggerWrapper.LogInformation("authorization: Bearer " + token, this.GetType().Name, nameof(GetCustomHeaders) + "()", null);

            return new Dictionary<string, string> { { "authorization", "Bearer " + token } };
        }
    }
}