using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Validation;
using GraphQLValidation;
using Microsoft.AspNetCore.Http;

public class GraphQLSettings
{
    public Func<HttpContext, Task<GraphQLUserContext>> BuildUserContext { get; set; }
    public object Root { get; set; }
    public List<IValidationRule> ValidationRules { get; } = new List<IValidationRule>();
}