using GraphQL.Execution;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphQL;

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
                        ((GraphQLUserContext) context.UserContext).RequestedProductIds.UnionWith(GetProductIdsFromArgs(new Arguments {arg}, context, variableValues)));
                }
            );
        }

        private IEnumerable<string> GetProductIdsFromArgs(Arguments args, ValidationContext context, Lazy<Variables> variableValues)
        {
            var argValues = new Lazy<Dictionary<string, object>>(() => ExecutionHelper.GetArgumentValues(
                context.Schema,
                new QueryArguments(
                    context.TypeInfo.GetFieldDef()?.Arguments
                        .Where(arg => arg.Name == "productIds")
                    ?? Enumerable.Empty<QueryArgument>()
                ),
                args,
                variableValues.Value));
            if (argValues.Value.ContainsKey("productIds"))
            {
                return ((IEnumerable) argValues.Value["productIds"]).Cast<string>();
            }

            return Enumerable.Empty<string>();
        }

    }
}
