using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Llm;

public interface IChunckCreater
{
    Task<string[]> ChunkCreate(DocumentDto documentDto);
}