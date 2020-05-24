using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TimeTrackerWeb.External;

namespace TimeTrackerWeb.Controllers
{
    public class BaseController<TEntity> : Controller
    {
        private IHttpClientWrapper<TEntity> _httpClientWrapper;

        protected IHttpClientWrapper<TEntity> HttpClientWrapper =>
            _httpClientWrapper ??
            (_httpClientWrapper = HttpContext?.RequestServices.GetService<IHttpClientWrapper<TEntity>>());
    }
}