using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.DocRead;

public interface IReaderSelector
{
    IReaders GetReader(ReaderType type);
}