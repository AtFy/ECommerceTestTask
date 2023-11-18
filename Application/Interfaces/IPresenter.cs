namespace Application.Interfaces;

public interface IPresenter
{
    public void ShowMenu(float progress);

    public void ShowAlreadyRunning();

    public void ShowDateIssue();

    public void ShowUnexpectedInputIssue();

    public void OnAnalysisCompletedShowResult(string result, (DateTime dateStart, DateTime dateFinish) dates);
}