using GoodEnough.Biology.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodEnough.Biology
{
    public class BodyBuilderContext
    {
        public IConfiguration Configuration { get; set; } = default!;
        public IServiceProvider Services { get; set; } = default!;
    }

    public class BodyBuilder : IBodyBuilder
    {
        private readonly IConfiguration config;
        private readonly BodyBuilderContext context;

        public BodyBuilder()
        {
            this.config = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            this.context = new BodyBuilderContext
            {
                Configuration = config,
            };
        }

        public IBodyPlan Build()
        {
            return null;
        }
    }
}
