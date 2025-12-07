using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Observers;

public interface ISubjectObserver
{
    void AddObserver(IAnalysisObserver observer);
    void AddChange(DocumentEntity documentEntity);
}