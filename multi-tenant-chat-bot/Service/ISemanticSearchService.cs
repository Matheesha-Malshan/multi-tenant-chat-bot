using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Service;

public interface ISemanticSearchService
{
    Task<string> SearchEmbeddings(SematicSearchingDto semanticSearchingDto);
}