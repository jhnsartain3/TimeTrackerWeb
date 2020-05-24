using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httwrap;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackerWeb.Controllers
{
    public class BaseDataAccessController<TEntity> : BaseController<TEntity>
    {
        public async Task<List<TEntity>> GetAll(string path, string token = null)
        {
            List<TEntity> result;

            try
            {
                result = await HttpClientWrapper.GetAllAsync(path, token);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve items", exception);
            }

            return result;
        }

        public async Task<TEntity> GetById(string path, string id, string token = null)
        {
            TEntity result;

            try
            {
                result = await HttpClientWrapper.GetByIdAsync(path, id, token);
            }
            catch (HttwrapException httwrapException)
            {
                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }

            if (result == null)
                throw new Exception("Failed to find item with ID: " + id);

            return result;
        }

        public async Task Post(string path, TEntity model, string token = null)
        {
            try
            {
                await HttpClientWrapper.PostAsync(path, model, token);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to create item", exception);
            }
        }

        public async Task<string> PostWithResultAsync(string path, TEntity model, string token = null)
        {
            try
            {
                return await HttpClientWrapper.PostWithResultAsync(path, model, token);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to create item", exception);
            }
        }

        public async Task<bool> Update(string path, string id, TEntity model, string token = null)
        {
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
                    throw new Exception("Failed to find item with ID: " + id, exception);
                }

                throw new DbUpdateConcurrencyException("Failed to update item ", dbUpdateConcurrencyException);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to update item ", exception);
            }
        }

        public async Task DeleteById(string path, string id, string token = null)
        {
            try
            {
                await HttpClientWrapper.DeleteAsync(path, id, token);
            }
            catch (HttwrapException httwrapException)
            {
                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }
        }
    }
}