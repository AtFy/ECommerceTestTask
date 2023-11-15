namespace Application.Interfaces;

public interface IPresenter
{
    public void ShowMenu(float progress);

    public void ShowAlreadyRunning();

    public void OnAnalysisCompletedShowResult(string result);
}