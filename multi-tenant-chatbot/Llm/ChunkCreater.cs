using multi_tenant_chatBot.DocRead;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Observers;

namespace multi_tenant_chatBot.Llm;

public class ChunkCreater:IChunckCreater
{
    private readonly IReaderSelector _readerSelector;

    public ChunkCreater(IReaderSelector readerSelector)
    {
        _readerSelector = readerSelector;
    }

    public Task<string[]> ChunkCreate(DocumentDto documentDto)
    {
        return _readerSelector.GetReader(documentDto.ReaderType)
            .CreateText(documentDto);
        
    }
}