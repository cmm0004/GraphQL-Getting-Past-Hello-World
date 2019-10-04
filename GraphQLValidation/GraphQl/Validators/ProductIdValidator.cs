using GraphQL.Execution;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLValidation.GraphQl.Validators
{
    public class ProductIdValidator : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            var variableValues = new Lazy<Variables>(() => ExecutionHelper.GetVariableValues(
                context.Document,
                context.Schema,
                context.TypeInfo.GetAncestors().OfType<Operation>().First().Variables,
                context.Inputs));

            return new EnterLeaveListener(listener =>
                {
                    listener.Match<Argument>(arg =>
                        ((GraphQLUserContext) context.UserContext).RequestedProductIds.UnionWith(GetUpasInArgs(new Arguments {arg}, context, variableValues)));
                }
            );
        }

        private IEnumerable<Guid> GetUpasInArgs(Arguments args, ValidationContext context, Lazy<Variables> variableValues)
        {
            var argValues = new Lazy<Dictionary<string, object>>(() => ExecutionHelper.GetArgumentValues(
                context.Schema,
                new QueryArguments(
                    context.TypeInfo.GetFieldDef()?.Arguments
                        .Where(arg => arg.Name == "productId")
                    ?? Enumerable.Empty<QueryArgument>()
                ),
                args,
                variableValues.Value));

            return args.SelectMany(arg => arg.Name switch
                {
                "productId" => argValues.Value?.GetValueOrDefault(arg.Name) switch
                    {
                    Guid val => Enumerable.Repeat(val, 1),
                    _ => Enumerable.Empty<Guid>(),
                    },
                _ => Enumerable.Empty<Guid>(),
                });
        }

    }
}
