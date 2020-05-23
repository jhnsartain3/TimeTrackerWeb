using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httwrap;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackerWeb.Controllers
{
    public class BaseDataAccessController<TEntity> : BaseController<TEntity>
    {
        public async Task<List<TEntity>> GetAll(string path)
        {
            List<TEntity> result;

            try
            {
                result = await HttpClientWrapper.GetAllAsync(path);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve items", exception);
            }

            return result;
        }

        public async Task<TEntity> GetById(string path, string id)
        {
            TEntity result;

            try
            {
                result = await HttpClientWrapper.GetById(path, id);
            }
            catch (HttwrapException httwrapException)
            {
                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }

            if (result == null)
                throw new Exception("Failed to find item with ID: " + id);

            return result;
        }

        public async Task<bool> Post(string path, TEntity model)
        {
            try
            {
                return await HttpClientWrapper.PostAsync(path, model);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to create item", exception);
            }
        }

        public async Task<bool> Update(string path, string id, TEntity model)
        {
            try
            {
                return await HttpClientWrapper.PutAsync(path, id, model);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                try
                {
                    if (await HttpClientWrapper.GetById(path, id) != null)
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

        public async Task DeleteById(string path, string id)
        {
            try
            {
                await HttpClientWrapper.DeleteAsync(path, id);
            }
            catch (HttwrapException httwrapException)
            {
                throw new HttwrapException("Failed to find item with ID: " + id, httwrapException);
            }
        }
    }
}