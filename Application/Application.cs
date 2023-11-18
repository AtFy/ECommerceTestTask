using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
using Application.Extensions;
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
            
            var userInput = Console.ReadLine();
            if (!Validator.CheckIfInputFormatCorrect(userInput))
            {
                continue;
            }
            
            if (userInput == CommandInterpritator.GetCommandEnterpritation(Commands.Stop))
            {
                return;
            }
            
            var userCommand = userInput?.Split('|')[0].TrimEnd();
            
            (DateTime dateStart, DateTime dateFinish) userDates = (DateTime.MinValue, DateTime.MinValue);
            try
            {
                userDates = userInput.GetDatesFromUserInput();
            }
            catch (ArgumentException)
            {
                _presenter.ShowDateIssue();
            }
            catch
            {
                _presenter.ShowUnexpectedInputIssue();
            }
            
            
            if (CommandInterpritator.CheckIfCommand(userCommand))
            {
                if (_logicWorker.IsRunning)
                {
                    _presenter.ShowAlreadyRunning();
                    continue;
                }

                _logicWorker.RunAnalysisAsync(CommandInterpritator.GetAnalyzer(userCommand), userDates);
            }
        }
    }
}