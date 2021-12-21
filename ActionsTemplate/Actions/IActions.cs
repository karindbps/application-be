using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;

namespace ActionsTemplate
{    public interface IAction
    {
        bool AppliesTo(string path);

        Task<string> DoAction(APIGatewayProxyRequest request);
    }
}
