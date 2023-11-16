using Application.Interfaces;

namespace Application;

public class CLIPresenter : IPresenter
{
    public CLIPresenter()
    {
        LogicWorker.AnalysisCompletedEvent += OnAnalysisCompletedShowResult;
        LogicWorker.ProgressedEvent += ShowMenu;
    }
    public void ShowMenu(float progress)
    {
        Console.Clear();
        
        PrintDivider();
        Console.WriteLine("Please, select the required option by typing a command in the CLI.\nPrint...\n" +
                          $"{CommandInterpritator.GetCommandEnterpritation(Commands.SqlAnalysis)}" +
                          " - to perform the analysis using SQL.\n" +
                          $"{CommandInterpritator.GetCommandEnterpritation(Commands.CsvAnalysis)}" +
                          " - to perform the analysis using raw CSV file.\n" +
                          $"{CommandInterpritator.GetCommandEnterpritation(Commands.Stop)}" +
                          $" - to exit the application. You can use \"stop\" command to exit application any time.");
        PrintLoadingBar(progress);
        PrintDivider();
    }

    private void PrintDivider()
    {
        for (var i = 0; i != 100; ++i)
        {
            Console.Write('=');
        }
        Console.WriteLine();
    }

    private void PrintLoadingBar(float progress)
    {
        Console.Write("\nProgress:");
        Console.Write("\n{");
        for (var i = 0; i != 100; ++i)
        {
            if (i < progress * 100)
            {
                Console.Write('|');
                continue;
            }
            Console.Write('.');
        }
        Console.Write("}\n");
    }

    public void ShowAlreadyRunning()
    {
        Console.WriteLine($"Process is already running. Can't start the 2nd one.");
        Thread.Sleep(3000);
    }
    
    public void OnAnalysisCompletedShowResult(string result)
    {
        Console.Clear();
        ShowMenu(1f);
        Console.WriteLine($"Here is the result: {result}\n" +
                          $"Press any key to proceed to main menu.");
        Console.ReadKey();
        
        ShowMenu(0f);
        
    }
}