using AspNet5WithFullFramework.Models;
using FluentNHibernate.Mapping;

namespace AspNet5WithFullFramework.Db.Maps
{
    public class TaskMap
        : ClassMap<Task>, IMap
    {
        public TaskMap()
        {
            Table("Tasks");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Length(1000).Not.Nullable();
        }
    }
}