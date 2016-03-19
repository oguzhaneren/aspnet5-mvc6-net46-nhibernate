using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNet5WithFullFramework.Db.Maps;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;

namespace AspNet5WithFullFramework.Db
{
    public static class NHibernateInstaller
    {
        public static void AddNHibernate(this IServiceCollection serviceCollection,IConfigurationRoot configurationRoot)
        {
            var nhibernateConfiguration = Fluently.Configure()
                   .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.Is(configurationRoot["Nhibernate:Con"])).Dialect<MsSql2012Dialect>())
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IMap>())
                   .ExposeConfiguration(c =>
                   {


                       c.SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, "true");
                       c.SetProperty(NHibernate.Cfg.Environment.PrepareSql, "true");
                       c.SetProperty("use_sql_comments", "false");


                       var update = new SchemaUpdate(c);
                       update.Execute(true, true);

                   })
                   .Cache(c => c
                       .UseQueryCache()
                       .UseSecondLevelCache()
                       .ProviderClass<HashtableCacheProvider>())
                   .BuildConfiguration();
            var sessionFactory = nhibernateConfiguration.BuildSessionFactory();

            serviceCollection.AddSingleton<Configuration>((sp)=> nhibernateConfiguration);
            serviceCollection.AddSingleton<ISessionFactory>(sp => sessionFactory);
            serviceCollection.AddScoped<ISession>(sp =>
            {
                var session = sessionFactory.OpenSession();
                session.BeginTransaction();
                return session;
            });

        }
    }
}
