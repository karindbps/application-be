using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.APIGatewayEvents;

namespace ActionsTemplate
{
    public class ActionStrategy : IActionStrategy
    {
        private readonly IEnumerable<IAction> _knownActions;

        public ActionStrategy(IEnumerable<IAction> knownActions)
        {
            _knownActions = knownActions;
        }

        public async Task<string> Execute(APIGatewayProxyRequest request)
        {
            //Obtain the action from the end of request.Path.
            string action = request.Path?.TrimEnd('/').Split('/')?.Last().ToLower();

            Console.WriteLine($"AdminService: Request Path requested action: {action}");

            //Find out what action
            IAction executor = _knownActions.FirstOrDefault(e => e.AppliesTo(action));

            if (executor == null)
                throw new ArgumentException($"AdminService: Unknown action requested {request.Path}");

            //Run the code for the action: request object or Body which is string
            return await executor.DoAction(request);
        }
    }
}
