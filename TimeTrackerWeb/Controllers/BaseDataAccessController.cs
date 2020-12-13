using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httwrap;
using Microsoft.EntityFrameworkCore;
using Sartain_Studios_Common.Logging;

namespace TimeTrackerWeb.Controllers
{
    public class BaseDataAccessController<TEntity> : BaseController<TEntity>
    {
        protected ILoggerWrapper _loggerWrapper;

        public BaseDataAccessController(ILoggerWrapper loggerWrapper)
        {
            _loggerWrapper = loggerWrapper;
        }

        public async Task<List<TEntity>> GetAll(string path, string token = null)
        {
            List<TEntity> result;

            _loggerWrapper.LogInformation("path: " + path, GetType().Name, nameof(GetAll) + "()", null);

            try
            {
                result = await HttpClientWrapper.GetAllAsync(path, token);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Failed to retrieve items", GetType().Name, nameof(GetAll) + "()", null);
                _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(GetAll) + "()", null);
                _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null, GetType().Name, nameof(GetAll) + "()", null);

                throw new Exception("Failed to retrieve items", exception);
            }

            return result;
        }

        public async Task<List<TEntity>> GetAllById(string path, string itemId, string token = null)
        {
            List<TEntity> result;

            _loggerWrapper.LogInformation("path: " + path + " " + "itemId: " + itemId, GetType().Name, nameof(GetAllById) + "()", null);

            try
            {
                result = await HttpClientWrapper.GetAllByIdAsync(path, itemId, token);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Failed to find items with ID: ", GetType().Name, nameof(GetAllById) + "()", null);
                _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(GetAllById) + "()", null);
                _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null, GetType().Name, nameof(GetAllById) + "()", null);

                throw new Exception("Failed to find items with ID: " + itemId, exception);
            }

            return result;
        }

        public async Task<List<T>> GetAllById<T>(string path, string itemId, string token = null)
        {
            List<T> result;

            _loggerWrapper.LogInformation("path: " + path + " " + "itemId: " + itemId, GetType().Name, nameof(GetAllById) + "()", null);

            try
            {
                result = await HttpClientWrapper.GetAllByIdAsync<T>(path, itemId, token);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Failed to find items with ID: " + itemId, GetType().Name, nameof(GetAllById) + "()", null);
                _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(GetAllById) + "()", null);
                _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null, GetType().Name, nameof(GetAllById) + "()", null);

                throw new Exception("Failed to find items with ID: " + itemId, exception);
            }

            return result;
        }

        public async Task<TEntity> GetById(string path, string id, string token = null)
        {
            TEntity result;

            _loggerWrapper.LogInformation("path: " + path + " " + "id: " + id, GetType().Name, nameof(GetById) + "()", null);

            try
            {
                result = await HttpClientWrapper.GetByIdAsync(path, id, token);
            }
            catch (HttwrapException httwrapException)
            {
                _loggerWrapper.LogError("Failed to find item with ID: " + id, GetType().Name, nameof(GetById) + "()", null);
                _loggerWrapper.LogError(httwrapException.Message, GetType().Name, nameof(GetById) + "()", null);
                _loggerWrapper.LogError(httwrapException.InnerException != null ? httwrapException.InnerException.Message : null, GetType().Name, nameof(GetById) + "()", null);

                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }

            if (result == null)
            {
                _loggerWrapper.LogError("Failed to find item with ID: " + id, GetType().Name, nameof(GetById) + "()", null);

                throw new Exception("Failed to find item with ID: " + id);
            }

            return result;
        }

        public async Task<T> GetById<T>(string path, string id, string token = null)
        {
            T result;

            _loggerWrapper.LogInformation("path: " + path + " " + "id: " + id, GetType().Name, nameof(GetById) + "()", null);

            try
            {
                result = await HttpClientWrapper.GetByIdAsync<T>(path, id, token);
            }
            catch (HttwrapException httwrapException)
            {
                _loggerWrapper.LogError("Failed to find item with ID: " + id, GetType().Name, nameof(GetById) + "()", null);
                _loggerWrapper.LogError(httwrapException.Message, GetType().Name, nameof(GetById) + "()", null);
                _loggerWrapper.LogError(httwrapException.InnerException != null ? httwrapException.InnerException.Message : null, GetType().Name, nameof(GetById) + "()", null);

                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }

            if (result == null)
            {
                _loggerWrapper.LogError("Failed to find item with ID: " + id, GetType().Name, nameof(GetById) + "()", null);

                throw new Exception("Failed to find item with ID: " + id);
            }

            return result;
        }

        public async Task Post(string path, TEntity model, string token = null)
        {
            _loggerWrapper.LogInformation("path: " + path, GetType().Name, nameof(Post) + "()", null);

            try
            {
                await HttpClientWrapper.PostAsync(path, model, token);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Failed to create item", GetType().Name, nameof(Post) + "()", null);
                _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(Post) + "()", null);
                _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null, GetType().Name, nameof(Post) + "()", null);

                throw new Exception("Failed to create item", exception);
            }
        }

        public async Task<string> PostWithResultAsync(string path, TEntity model, string token = null)
        {
            _loggerWrapper.LogInformation("path: " + path, GetType().Name, nameof(PostWithResultAsync) + "()", null);

            try
            {
                return await HttpClientWrapper.PostWithResultAsync(path, model, token);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Failed to create item", GetType().Name, nameof(PostWithResultAsync) + "()", null);
                _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(PostWithResultAsync) + "()", null);
                _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null, GetType().Name, nameof(PostWithResultAsync) + "()", null);

                throw new Exception("Failed to create item", exception);
            }
        }

        public async Task<bool> Update(string path, string id, TEntity model, string token = null)
        {
            _loggerWrapper.LogInformation("path: " + path + " " + "id: " + id, GetType().Name, nameof(Update) + "()", null);

            try
            {
                return await HttpClientWrapper.PutAsync(path, id, model, token);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                try
                {
                    if (await HttpClientWrapper.GetByIdAsync(path, id, token) != null)
                        throw new Exception("Failed to update item ");
                }
                catch (Exception exception)
                {
                    _loggerWrapper.LogError("Failed to find item with ID: " + id, GetType().Name, nameof(Update) + "()", null);
                    _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(GetById) + "()", null);
                    _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null, GetType().Name, nameof(Update) + "()", null);

                    throw new Exception("Failed to find item with ID: " + id, exception);
                }

                throw new DbUpdateConcurrencyException("Failed to update item ", dbUpdateConcurrencyException);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Failed to update item", GetType().Name, nameof(Update) + "()", null);
                _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(Update) + "()", null);
                _loggerWrapper.LogError(exception.InnerException != null ? exception.InnerException.Message : null,
                    GetType().Name, nameof(Update) + "()", null);

                throw new Exception("Failed to update item ", exception);
            }
        }

        public async Task DeleteById(string path, string id, string token = null)
        {
            _loggerWrapper.LogInformation("path: " + path + " " + "id: " + id, GetType().Name, nameof(DeleteById) + "()", null);

            try
            {
                await HttpClientWrapper.DeleteAsync(path, id, token);
            }
            catch (HttwrapException httwrapException)
            {
                _loggerWrapper.LogError("Failed to find item with ID: " + id, GetType().Name, nameof(DeleteById) + "()", null);
                _loggerWrapper.LogError(httwrapException.Message, GetType().Name, nameof(DeleteById) + "()", null);
                _loggerWrapper.LogError(httwrapException.InnerException != null ? httwrapException.InnerException.Message : null, GetType().Name, nameof(DeleteById) + "()", null);

                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }
        }
    }
}