using System.Reflection.Metadata.Ecma335;
using Application.Interfaces;
using Lib.Analyzer;

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
            if (userCommand == CommandInterpritator.GetCommandEnterpritation(Commands.Stop))
            {
                return;
            }

            if (CommandInterpritator.CheckIfCommand(userCommand))
            {
                if (_logicWorker.IsRunning)
                {
                    _presenter.ShowAlreadyRunning();
                    continue;
                }

                _logicWorker.RunAnalysisAsync(CommandInterpritator.GetAnalyzer(userCommand));
            }
        }
    }
}