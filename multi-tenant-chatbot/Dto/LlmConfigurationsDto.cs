namespace multi_tenant_chatBot.Dto;

public class LlmConfigurationsDto
{
    public int ChatBotId { get; set; }
    public LlmModelsDto ModelName { get; set; }
    public double Temperature { get; set; }
    public int MaxToken { get; set; }
    public double TopP { get; set; }

    public LlmConfigurationsDto(int chatBotId, LlmModelsDto modelName,
        double temperature, int maxToken, double topP)
    {
        ChatBotId = chatBotId;
        ModelName = modelName;
        Temperature = temperature;
        MaxToken = maxToken;
        TopP = topP;
    }
}