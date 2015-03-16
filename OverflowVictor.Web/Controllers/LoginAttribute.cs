using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using log4net;
using log4net.Repository.Hierarchy;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using IActionFilter = System.Web.Mvc.IActionFilter;

namespace OverflowVictor.Web.Controllers
{
    public class LoginAttribute : FilterAttribute, IActionFilter, IResultFilter, IAuthorizationFilter, IExceptionFilter
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoginAttribute));
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            throw new System.NotImplementedException();
        }

        public void LogEvents()
        {
            logger.Debug("Debugging Message");
            logger.Info("Info Messsage");
            logger.Warn("Warning Message");
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string action = filterContext.ActionDescriptor.ActionName;
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            logger.Info(action+controller);
        }

        public void OnException(ExceptionContext filterContext)
        {
            string m = filterContext.Exception.Message;
            logger.Error(m);

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new System.NotImplementedException();
        }
    }
}