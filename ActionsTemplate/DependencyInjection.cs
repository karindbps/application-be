using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ActionsTemplate
{
    class DependencyInjection
    {
        public static IServiceCollection Container => ConfigureServices();

        private static IServiceCollection ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            //add a line for each 'iaction' added here:
            services.AddSingleton<IAction, UploadPhotoAction>();

            services.AddSingleton<IActionStrategy, ActionStrategy>(provider =>
            {
                IEnumerable<IAction> actions = provider.GetServices<IAction>();
                return new ActionStrategy(actions);
            });

            return services;
        }
    }
}
