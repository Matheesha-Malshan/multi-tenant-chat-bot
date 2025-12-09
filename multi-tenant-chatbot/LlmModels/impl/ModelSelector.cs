using System;
using System.Collections.Generic;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.LlmModels.impl;

public class ModelSelector:IModelSelector
{
    private Dictionary<LlmModelsDto,IModels> _llmModels=new Dictionary<LlmModelsDto,IModels>();

    public ModelSelector(IEnumerable<IModels> models)
    {
        foreach (var model in models)
        {
            foreach (LlmModelsDto llmModelsDto in Enum.GetValues(typeof(LlmModelsDto)))
            {
                if (model.CheckModelType(llmModelsDto))
                {
                    _llmModels.Add(llmModelsDto,model);
                }
            }
        }
    }

    public IModels GetModels(LlmModelsDto llmModelsDto)
    {
        return _llmModels[llmModelsDto];
    }
}