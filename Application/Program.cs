using Application.Interfaces;
using Autofac;

namespace Application;

public static class Program
{
    public static void Main()
    {
        var container = ContainerConfigurator.Configure();
        using var scope = container.BeginLifetimeScope();
        var app = scope.Resolve<IApplication>();
        app.Run();

        Console.ReadKey();
    }
}