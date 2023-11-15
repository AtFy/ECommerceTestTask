using Application.Interfaces;
using Lib.Analyzer.Interfaces;

namespace Application;

public class LogicWorker : ILogicWorker
{
    public LogicWorker(IDbController dbController)
    {
        _dbController = dbController;
    }

    private IDbController _dbController;
    public string RunSqlAnalysis()
    {
        throw new NotImplementedException();
    }

    public string RunNoSqlAnalysis()
    {
        throw new NotImplementedException();
    }
}