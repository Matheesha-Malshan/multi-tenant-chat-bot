using System;

namespace multi_tenant_chatBot.ApiService;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiredApiKeyAttribute:Attribute
{
    
}