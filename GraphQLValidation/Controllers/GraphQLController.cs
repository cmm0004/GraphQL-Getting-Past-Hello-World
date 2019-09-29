using GraphQL;
using GraphQL.Server.Transports.AspNetCore.Common;
using GraphQL.Types;
using GraphQL.Validation;
using GraphQLValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importer.Provider.Data.GraphQL
{
    [Route("[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly ILogger<GraphQLController> _logger;
        private readonly IEnumerable<IValidationRule> _rules;
        public GraphQLController(ISchema schema, IDocumentExecuter documentExecutor, ILogger<GraphQLController> logger, IEnumerable<IValidationRule> rules)
        {
            _documentExecuter = documentExecutor ?? throw new ArgumentNullException(nameof(documentExecutor));
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            var result = await _documentExecuter.ExecuteAsync(options =>
            {
                options.Schema = _schema;
                options.Query = query.Query;
                options.Inputs = query.Variables.ToInputs();
                options.UserContext = new GraphQLUserContext(Request);
                options.ValidationRules = _rules.Concat(DocumentValidator.CoreRules());
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            });

            if (!(result.Errors?.Count > 0))
            {
                return Ok(result);
            }

            foreach (var err in result.Errors)
            {
                _logger.LogError(err, "Error occurred inside documentExecuter");
            }

            return BadRequest(result);
        }
    }
}