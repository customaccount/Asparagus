using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebJobs.Extensions.RabbitMQ.Config
{
    public static class RabbitMqWebJobsBuilderExtensions
    {
        internal const string ConfigSectionName = "RabbitMq";

        public static IWebJobsBuilder AddRabbitMq(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<RabbitMqExtensionConfigProvider>()
                .ConfigureOptions<RabbitMqOptions>((config, path, options) =>
                {
                    options.ServerEndpoint = config?.GetSection("ServerEndpoint")?[Constants.DefaultServerEndpointName];
                    IConfigurationSection section = config?.GetSection(path);
                    section.Bind(options);
                });

            return builder;
        }

        /// <summary>
        /// Adds the RabbitMq extension to the provided <see cref="IWebJobsBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IWebJobsBuilder"/> to configure.</param>
        /// <param name="configure">An <see cref="Action{RabbitMqOptions}"/> to configure the provided <see cref="RabbitMqOptions"/>.</param>
        public static IWebJobsBuilder AddRabbitMq(this IWebJobsBuilder builder, Action<RabbitMqOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddRabbitMq();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
