using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ActionsTemplate
{
    public class Handler
    {
        private readonly IActionStrategy _actionStrategy;

        public Handler() : this(DependencyInjection.Container.BuildServiceProvider()) { }

        public Handler(IServiceProvider serviceProvider)
        {
            _actionStrategy = serviceProvider.GetService<IActionStrategy>();
        }

        public async Task<APIGatewayProxyResponse> HandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            //Prepare headers for response
            IDictionary<string, string> responseHeaders = new Dictionary<string, string>
            {
            };

            try
            {
                //Call ActionStrategy
                string resp = await _actionStrategy.Execute(request);

                Console.WriteLine($"Service SUCCESS: {resp}");

                return new APIGatewayProxyResponse()
                {
                    StatusCode = 200,
                    Headers = responseHeaders,
                    Body = resp
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Handler.HandlerAsync() ERROR: " + ex.Message + ex.StackTrace);
                return new APIGatewayProxyResponse()
                {
                    StatusCode = 500,
                    Headers = responseHeaders,
                    Body = JsonSerializer.Serialize(ex.Message)
                };
            }
        }
    }
}
