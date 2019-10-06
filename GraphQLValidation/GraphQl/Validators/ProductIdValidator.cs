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
    /*
     * 'validator' is a strong word, the validators are really just event handlers, you could execute any arbitrary function in here on a node
     */

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
                    // match a node type, arguments are considered a node the same as everything else in the query.
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

            if (argValues.IsValueCreated && argValues.Value.ContainsKey("productIds"))
            {
                return ((IEnumerable) argValues.Value["productIds"]).Cast<string>();
            }

            return Enumerable.Empty<string>();
        }

    }
}
