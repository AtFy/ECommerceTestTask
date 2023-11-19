namespace Lib.Analyzer;

public static class FileValidator
{
    public static bool CheckIfValid(string filePath) =>
        CheckIfFilePathCorrect(filePath) &&
        CheckIfCsv(filePath) &&
        CheckIfNotEmpty(filePath);
    
    private static bool CheckIfFilePathCorrect(string filePath) => File.Exists(filePath);
    
    private static bool CheckIfCsv(string filePath) => Path.GetExtension(filePath) == ".csv";

    private static bool CheckIfNotEmpty(string filePath) => new FileInfo(filePath).Length != 0;
}