using System;
using System.Collections.Generic;
using System.Linq;
using AspNet5WithFullFramework.Models;
using FluentNHibernate.Mapping;

namespace AspNet5WithFullFramework.Db.Maps
{
    public interface IMap
    {
    }

    public class TaskMap
        : ClassMap<Task>, IMap
    {
        public TaskMap()
        {
            Table("Tasks");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
        }
    }
}
