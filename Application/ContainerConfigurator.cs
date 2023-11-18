using System.Reflection;
using Application.Interfaces;
using Autofac;
using Lib.Analyzer.Interfaces;
using Lib.DbController;
using Lib.DbController.Interfaces;

namespace Application;

public static class ContainerConfigurator
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Application>().As<IApplication>().SingleInstance();
        builder.RegisterType<LogicWorker>().As<ILogicWorker>().SingleInstance();
        builder.RegisterType<CliPresenter>().As<IPresenter>().SingleInstance();
        builder.RegisterAssemblyTypes(Assembly.Load(nameof(Lib.Analyzer)))
            .Where(type => !type.Namespace.Contains("Interfaces"))
            .As<IAnalyzer>();
        builder.RegisterType<DbController>().As<IDbController>();
        builder.RegisterType<ResilientDbController>().As<IDbController>();

        return builder.Build();
    }
}