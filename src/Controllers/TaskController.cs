using System;
using AspNet5WithFullFramework.Models;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;
using NHibernate;

namespace AspNet5WithFullFramework.Controllers
{
    public class TaskController : Controller
    {
        private readonly ISession _session;

        public TaskController(ISession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            _session = session;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _session.QueryOver<Task>().List();
            return new ObjectResult(tasks);
        }

        [HttpPost]
        public IActionResult Post([FromBody]JObject obj)
        {
            var task = new Task()
            {
                Name = obj["name"].ToString()
            };
            _session.Save(task);
            return new ObjectResult(task);
        }
    }
}