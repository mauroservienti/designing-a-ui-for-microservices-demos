using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ITOps.UIComposition.Mvc
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddUIComposition(this IMvcBuilder builder, string assemblySearchPattern = "*ViewComponents*.dll")
        {
            var fileNames = Directory.GetFiles(AppContext.BaseDirectory, assemblySearchPattern);

            var assemblies = new List<(string BaseNamespace, Assembly Assembly)>();

            foreach (var fileName in fileNames)
            {
                var assembly = Assembly.LoadFrom(fileName);
                var attribute = assembly.GetCustomAttribute<UICompositionSupportAttribute>();

                if (attribute != null)
                {
                    assemblies.Add((attribute.BaseNamespace, assembly));
                }
            }

            assemblies.ForEach(a =>
            {
                builder.Services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.FileProviders.Add(new EmbeddedFileProvider(a.Assembly, a.BaseNamespace));
                });
                builder.AddApplicationPart(a.Assembly);
            });

            return builder;
        }
    }
}
