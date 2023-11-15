using System.Reflection.Metadata.Ecma335;
using Application.Interfaces;

namespace Application;

public class Application : IApplication
{
    public Application(ILogicWorker logicWorker, IPresenter presenter)
    {
        _logicWorker = logicWorker;
        _presenter = presenter;
    }

    private ILogicWorker _logicWorker;
    
    private IPresenter _presenter;
    
    public void Run()
    {
        while (true)
        {
            _presenter.ShowMenu(_logicWorker.Progress);
            
            var userCommand = Console.ReadLine();
            if (userCommand == CommandInterpritator.GetCommandEnterpritation(Commands.SqlAnalysis))
            {
                if (_logicWorker.IsRunning)
                {
                    _presenter.ShowAlreadyRunning();
                    continue;
                }
                _logicWorker.RunSqlAnalysisAsync();
            }

            if (userCommand == CommandInterpritator.GetCommandEnterpritation(Commands.NoSqlAnalysis))
            {
                if (_logicWorker.IsRunning)
                {
                    _presenter.ShowAlreadyRunning();
                    continue;
                }
                _logicWorker.RunNoSqlAnalysisAsync();
            }

            if (userCommand == CommandInterpritator.GetCommandEnterpritation(Commands.Stop))
            {
                return;
            }
        }
    }
}