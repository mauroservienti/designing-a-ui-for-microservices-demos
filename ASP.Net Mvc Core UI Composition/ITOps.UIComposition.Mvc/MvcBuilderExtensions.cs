using ITOps.ViewModelComposition;
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

            var assemblies = Create( new
            {
                BaseNamespace = "",
                Assembly = (Assembly)null
            } );

            foreach (var fileName in fileNames)
            {
                var assembly = AssemblyLoader.Load(fileName);
                var attribute = assembly.GetCustomAttribute<UICompositionSupportAttribute>();

                if (attribute != null)
                {
                    assemblies.Add(new
                    {
                        BaseNamespace = attribute.BaseNamespace,
                        Assembly = assembly
                    });
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

        static List<T> Create<T>(T template)
        {
            return new List<T>();
        }
    }
}
