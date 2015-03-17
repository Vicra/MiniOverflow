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
            
        }

        public void LogEvents()
        {
            logger.Debug("Debugging Message");
            logger.Info("Info Messsage");
            logger.Warn("Warning Message");
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string action = filterContext.ActionDescriptor.ActionName;
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            logger.Info(action+controller);
        }
        /*
        public void OnException(ExceptionContext filterContext)
        {
            string m = filterContext.Exception.Message;
            logger.Error(m);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult();

        }
          */

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}