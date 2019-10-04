using GraphQL;
using GraphQL.Server.Transports.AspNetCore.Common;
using GraphQLValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Importer.Provider.Data.GraphQL
{
    // normally you would use the magic middleware the library offers you,
    // but its much easier to understand whats going on if i caught the request in a controller,
    // not some decompiled middleware code
    // but basic procedure: request comes in, matches the route, set options on the document executor, execute the document, return errors/ data
    [Route("[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ExecutionOptions _options;
        public GraphQLController(IDocumentExecuter documentExecutor, ExecutionOptions options)
        {
            _documentExecuter = documentExecutor ?? throw new ArgumentNullException(nameof(documentExecutor));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            _options.Query = query.Query;
            _options.UserContext = new GraphQLUserContext(Request);

            var result = await _documentExecuter.ExecuteAsync(_options);

            return Ok(new { Data = result.Data, Errors = result.Errors?.Select(err => new { Message = err.Message, Locations = err.Locations }) });
           
        }
    }
}