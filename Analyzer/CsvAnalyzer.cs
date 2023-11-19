using System.Globalization;
using Lib.Analyzer.Extensions;
using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public delegate void CsvAnalysisStartedEventHandler();

public enum Cols
{
    EventType,
    EventTime,
    Price,
    Brand,
    CategoryId,
    ProductId
}

public class CsvAnalyzer : IAnalyzer
{
    public static event CsvAnalysisStartedEventHandler CsvAnalysisStartedEvent;
    public event AnalysisStepProgressedEventHandler AnalysisProgressedEvent;

    private string? _filePath;

    private int _fileSize;

    private int _progressStepSize; 

    private Dictionary<Cols, int> _colsPositions = new()
    {
        { Cols.EventType, -1 },
        { Cols.EventTime, -1 },
        { Cols.Price, -1 },
        { Cols.Brand, -1 },
        { Cols.CategoryId, -1 },
        { Cols.ProductId, -1 }
    };

    private Dictionary<Cols, string> _colsInterpritations = new()
    {
        { Cols.EventType, "event_type" },
        { Cols.EventTime, "event_time" },
        { Cols.Price, "price" },
        { Cols.Brand, "brand" },
        { Cols.CategoryId, "category_id" },
        { Cols.ProductId, "product_id" }
    };

    private decimal _totalGrossForPeriod = 0;
    
    private Dictionary<string, long> _brandsLeaderBoard = new();

    private Dictionary<string, long> _categoriesLeaderBoard = new();

    private Dictionary<string, long> _productsLeaderBoard = new();
    
    public async Task<string> RunAnalysisAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        CsvAnalysisStartedEvent.Invoke();
        GetFilePath();

        if (!FileValidator.CheckIfValid(_filePath))
        {
            return "Invalid file received.";
        }

        _fileSize = await GetLinesCountAsync();
        _progressStepSize = GetProgressStepSize();
        
        SetColsPositions(File.ReadLines(_filePath).First());
        
        
        await Analyze(dates);
        
        
        GC.Collect();
        return $"> Total gross for the period is ${_totalGrossForPeriod}\n" +
               $"> The most popular brand during the period is <{_brandsLeaderBoard
                   .Aggregate((x, y) => x.Value > y.Value ? x : y).Key}>\n" +
               $"> The most popular category Id during the period is <{_categoriesLeaderBoard
                   .Aggregate((x, y) => x.Value > y.Value ? x : y).Key}>\n" +
               $"> The most popular product Id during the period is <{_productsLeaderBoard
                   .Aggregate((x, y) => x.Value > y.Value ? x : y).Key}>\n";
    }
    
    private async Task Analyze((DateTime dateStart, DateTime dateFinish) dates)
    {
        var progressCounter = 0;
        var badDateCounter = 0;
        
        foreach (var line in File.ReadLines(_filePath).Skip(1))
        {
            if (progressCounter % _progressStepSize == 0)
            {
                AnalysisProgressedEvent.Invoke(0.01f);
            }
            
            var row = line.AsList();
            
            // Assuming that the dataset is sorted by date. 
            if (!CheckIfStaisfyDate(row, dates))
            {
                badDateCounter++;
                if (badDateCounter > 10)
                {
                    return;
                }
                progressCounter++;
            }
            
            AppendTotalGrossData(row);
            AppendMostPopularBrand(row);
            AppendMostPopularCategory(row);
            AppendMostPopularProduct(row);

            progressCounter++;
        }

    }
    
    private async Task<int> GetLinesCountAsync() => File.ReadLines(_filePath).Count();

    private int GetProgressStepSize() => _fileSize / 100;
    
    private void GetFilePath() => _filePath = Console.ReadLine();

    private void SetColsPositions(string headerRow)
    {
        var headerRowAsList = headerRow.AsList();

        foreach (var col in _colsPositions.Keys)
        {
            _colsPositions[col] = headerRowAsList.IndexOf(_colsInterpritations[col]);
        }
    }

    private bool CheckIfStaisfyDate(List<string> row, (DateTime dateStart, DateTime dateFinish) dates)
    {
        DateTime.TryParseExact(row[_colsPositions[Cols.EventTime]],
                "yyyy-MM-dd HH:mm:ss 'UTC'",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date); 
        return date >= dates.dateStart && date <= dates.dateFinish;
    }

    private void AppendTotalGrossData(List<string> row)
    {
        if (row[_colsPositions[Cols.EventType]] != "purchase")
        {
            return;
        }
        
        _totalGrossForPeriod += Decimal.Parse(row[_colsPositions[Cols.Price]], CultureInfo.InvariantCulture);
    }

    private void AppendMostPopularBrand(List<string> row)
    {
        if (row[_colsPositions[Cols.Brand]] == string.Empty)
        {
            return;
        }
        
        if (!_brandsLeaderBoard.ContainsKey(row[_colsPositions[Cols.Brand]]))
        {
            _brandsLeaderBoard.Add(row[_colsPositions[Cols.Brand]], 1);
        }

        _brandsLeaderBoard[row[_colsPositions[Cols.Brand]]]++;
    }

    private void AppendMostPopularCategory(List<string> row)
    {
        if (row[_colsPositions[Cols.CategoryId]] == string.Empty)
        {
            return;
        }
        
        if (!_categoriesLeaderBoard.ContainsKey(row[_colsPositions[Cols.CategoryId]]))
        {
            _categoriesLeaderBoard.Add(row[_colsPositions[Cols.CategoryId]], 1);
        }

        _categoriesLeaderBoard[row[_colsPositions[Cols.CategoryId]]]++;
    }
    
    private void AppendMostPopularProduct(List<string> row)
    {
        if (row[_colsPositions[Cols.ProductId]] == string.Empty)
        {
            return;
        }
        
        if (!_productsLeaderBoard.ContainsKey(row[_colsPositions[Cols.ProductId]]))
        {
            _productsLeaderBoard.Add(row[_colsPositions[Cols.ProductId]], 1);
        }

        _productsLeaderBoard[row[_colsPositions[Cols.ProductId]]]++;
    }
}