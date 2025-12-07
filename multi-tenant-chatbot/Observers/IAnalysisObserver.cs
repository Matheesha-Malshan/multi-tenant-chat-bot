using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Observers;

public interface IAnalysisObserver
{
    void Analyze(DocumentEntity  documentEntity);
}