using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Terraform;

namespace Cake.Terraform
{
    public class TerraformEnvListRunner : TerraformRunner<TerraformEnvListSettings>
    {
        public TerraformEnvListRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        public List<string> Run(TerraformEnvListSettings settings)
        {
            var builder =
                new ProcessArgumentBuilder()
                    .Append("env")
                    .Append("list");

            Run(settings, builder);

            var processSettings = new ProcessSettings
            {
                RedirectStandardOutput = true
            };

            var result = new List<string>();
            this.Run(settings, builder, processSettings, x =>
            {
                result = x.GetStandardOutput()
                    .Select(env => env.Replace("*", "").Trim())
                    .ToList();
            });

            return result;
        }
    }
}