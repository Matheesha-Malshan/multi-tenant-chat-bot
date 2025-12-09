using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Observers;

public interface ISubjectObserver
{
    void AddObserver(IAnalysisObserver observer);
    Task AddChange(AnalysisDto dto);
}