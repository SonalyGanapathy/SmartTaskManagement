using System;
using System.Web;
using System.Web.Mvc;

namespace SmartTaskManagement.Filters
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public SessionAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userId = httpContext.Session["UserId"];
            var role = httpContext.Session["UserRole"] as string;

            if (userId == null) return false;
            if (_roles.Length > 0 && Array.IndexOf(_roles, role) < 0)
                return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
