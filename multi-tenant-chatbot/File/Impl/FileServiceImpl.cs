using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.File.Impl;

public class FileServiceImpl: IFileService
{
    private string _baseDirectory = "wwwroot";
    private readonly ILogger<FileServiceImpl> _logger;

    public FileServiceImpl(ILogger<FileServiceImpl> logger)
    {
        _logger = logger;
    }

    public async Task SaveFile(DocumentDto documentDto)
    {
        string dirPath = _baseDirectory + "/" + documentDto.Id;
        
        if (!Directory.Exists(dirPath))
        {
            try
            {
                Directory.CreateDirectory(dirPath);
                _logger.LogInformation($"Created directory {dirPath}");
            }
            catch (NotSupportedException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        await CreateFile(dirPath,documentDto);
    }

    public string CreatePath(DocumentDto document)
    {
        return _baseDirectory+"/"+document.Id+"/"+document.File.FileName;
    }

    public async Task CreateFile(string dirPath, DocumentDto documentDto)
    {
        string path = _baseDirectory+"/"+documentDto.Id+"/"+documentDto.File.FileName;
        
        using (var stream = new FileStream(path, FileMode.Create))
        {
            try
            {
                await documentDto.File.CopyToAsync(stream); 
                _logger.LogInformation($"Created file {path}");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}