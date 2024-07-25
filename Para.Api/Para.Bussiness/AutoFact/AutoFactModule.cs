using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module = Autofac.Module;
using Autofac;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using System.Data;
using Para.Bussiness.Cqrs;
using System.Reflection;
using MediatR;

namespace Para.Bussiness.AutoFact
{
    public class AutoFactModule : Module
    {
        private readonly IConfiguration _configuration;

        public AutoFactModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionStringSql = _configuration.GetConnectionString("MsSqlConnection");

            builder.Register(c => new SqlConnection(connectionStringSql))
                .As<IDbConnection>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ParaDbContext(new DbContextOptionsBuilder<ParaDbContext>()
                .UseSqlServer(connectionStringSql)
                .Options))
                .AsSelf()
                .InstancePerLifetimeScope();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });

            builder.RegisterInstance(mapperConfig.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            }).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}