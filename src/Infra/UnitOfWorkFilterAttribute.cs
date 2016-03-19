using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc.Filters;
using NHibernate;

namespace AspNet5WithFullFramework.Infra
{
    public class UnitOfWorkFilterAttribute
        : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var session = context.HttpContext.RequestServices.GetService(typeof(ISession)) as ISession;
            if (session == null)
                return;
            session.Transaction.Commit();
        }


    }
}
