﻿using System.Reflection;
using Application.Interfaces;
using Autofac;

namespace Application;

public static class ContainerConfigurator
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Application>().As<IApplication>();
        builder.RegisterType<LogicWorker>().As<ILogicWorker>();
        builder.RegisterAssemblyTypes(Assembly.Load(nameof(Lib.Analyzer)))
            .Where(type => !type.Namespace.Contains("Interfaces"))
            .As(type => type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}"));

        return builder.Build();
    }
}