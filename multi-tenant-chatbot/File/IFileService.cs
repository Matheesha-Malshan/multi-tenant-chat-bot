using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.File;

public interface IFileService
{
    Task SaveFile(DocumentDto documentDto);
    string CreatePath(DocumentDto document);
}