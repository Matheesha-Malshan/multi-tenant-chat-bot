using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Observers.Impl;

public class DocAndChunksObserver:IAnalysisObserver
{
    private readonly ISubjectObserver _observer;

    public DocAndChunksObserver(ISubjectObserver observer)
    {
        _observer = observer;
        AddObserver();
      
    }
    public void AddObserver()
    {
        _observer.AddObserver(this);
    }
    public void Analyze(DocumentEntity documentEntity)
    {
        var a=new AnalysisEntity(
            1,
            0,
            1,
            documentEntity.Chunks,
        );
    }
}