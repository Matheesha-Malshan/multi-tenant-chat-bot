using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Service;

public interface IDocumentService
{
    Task CreateDocument(DocumentDto documentDto);
}