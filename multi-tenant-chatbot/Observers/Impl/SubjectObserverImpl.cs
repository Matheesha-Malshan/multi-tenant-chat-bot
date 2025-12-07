using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Observers.Impl;

public class SubjectObserverImpl:ISubjectObserver
{
    private List<IAnalysisObserver> _observers=new();
    

    public void AddObserver(IAnalysisObserver observer)
    {
        _observers.Add(observer);
    }

    public async Task AddChange(AnalysisDto  analysis)
    {
        await NotifyObservers(analysis);
    }

    public async Task NotifyObservers(AnalysisDto  analysis)
    {
       
        foreach (var observer in _observers)
        {
            await observer.Analyze(analysis);
        }
    }

}