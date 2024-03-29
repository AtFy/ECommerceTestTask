﻿using Application.Interfaces;
using Lib.Analyzer;

namespace Application;

public class CliPresenter : IPresenter
{
    public CliPresenter()
    {
        LogicWorker.AnalysisCompletedEvent += OnAnalysisCompletedShowResult;
        LogicWorker.ProgressedEvent += ShowMenu;
        CsvAnalyzer.CsvAnalysisStartedEvent += OnCsvAnalysisStartedAskPath;
    }
    public void ShowMenu(float progress)
    {
        Console.Clear();
        
        PrintDivider();
        Console.WriteLine("Please, select the required option by typing a command in the CLI.\nPrint...\n" +
                          $"\"{CommandInterpritator.GetCommandEnterpritation(Commands.SqlAnalysis)}" +
                          $" | dd.mm.yyyy-dd.mm.yyyy\"" +
                          " - to perform the analysis using SQL.\n" +
                          $"\"{CommandInterpritator.GetCommandEnterpritation(Commands.CsvAnalysis)}" +
                          $" | dd.mm.yyyy-dd.mm.yyyy\"" +
                          " - to perform the analysis using raw CSV file.\n" +
                          $"\"{CommandInterpritator.GetCommandEnterpritation(Commands.Stop)}\"" +
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

    public void ShowDateIssue()
    {
        Console.WriteLine($"Date period is invalid. Fist date should be lower than the 2nd.");
        Thread.Sleep(3000);
    }

    public void ShowUnexpectedInputIssue()
    {
        Console.WriteLine($"Unexpected error appeared while reading command.");
        Thread.Sleep(3000);
    }
    
    public void OnAnalysisCompletedShowResult(string result, (DateTime dateStart, DateTime dateFinish) dates)
    {
        Console.Clear();
        ShowMenu(1f);
        Console.WriteLine($"Results ({dates.dateStart.ToShortDateString()}-{dates.dateFinish.ToShortDateString()}):\n{result}\n" +
                          $"Press any key to proceed to main menu.");
    }

    private void OnCsvAnalysisStartedAskPath()
    {
        Thread.Sleep(10);
        Console.WriteLine("Please press enter, and then, provide a full file path.");
    }
}