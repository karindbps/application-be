using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;

namespace ActionsTemplate
{
    public interface IActionStrategy
    {
        Task<string> Execute(APIGatewayProxyRequest request);
    }
}