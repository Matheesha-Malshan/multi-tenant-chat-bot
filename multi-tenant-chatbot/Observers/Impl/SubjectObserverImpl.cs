using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Observers.Impl;

public class SubjectObserverImpl:ISubjectObserver
{
    private List<IAnalysisObserver> _observers;
    public SubjectObserverImpl()
    {
        _observers=new List<IAnalysisObserver>();
    }

    public void AddObserver(IAnalysisObserver observer)
    {
        _observers.Add(observer);
    }

    public void AddChange(DocumentEntity documentEntity)
    {
        NotifyObservers(documentEntity);
    }

    public void NotifyObservers(DocumentEntity documentEntity)
    {
        foreach (var observer in _observers)
        {
            observer.Analyze(documentEntity);
        }
    }

}