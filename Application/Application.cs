using Application.Interfaces;

namespace Application;

public class Application : IApplication
{
    public Application(ILogicWorker logicWorker)
    {
        _logicWorker = logicWorker;
    }

    private ILogicWorker _logicWorker;

    public void Run()
    {
        
    }
}