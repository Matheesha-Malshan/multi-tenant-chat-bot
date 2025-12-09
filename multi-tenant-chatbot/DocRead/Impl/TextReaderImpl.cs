using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.DocRead.Impl;

public class TextReaderImpl:IReaders
{ 
    public async Task<string[]> CreateText(DocumentDto doc)
    {
        using var reader = new StreamReader((Stream)doc.File.OpenReadStream());
    
        string text = await reader.ReadToEndAsync();

        string[] sentences = text
            .Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();

        return sentences;

    }
    public bool CheckType(ReaderType type)
    {
        return type==ReaderType.Text;
    }
    
}