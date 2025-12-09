using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.DocRead;

public interface IReaders
{
    Task<string[]> CreateText(DocumentDto doc);
    bool CheckType(ReaderType type);
}