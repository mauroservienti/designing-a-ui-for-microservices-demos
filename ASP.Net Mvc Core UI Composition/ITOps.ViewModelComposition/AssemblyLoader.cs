using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System;

namespace ITOps.ViewModelComposition
{
    public static class AssemblyLoader
    {
        public static Assembly Load(string assemblyFullPath)
        {
            var fileNameWithOutExtension = Path.GetFileNameWithoutExtension(assemblyFullPath);

            var inCompileLibraries= DependencyContext.Default.CompileLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));
            var inRuntimeLibraries = DependencyContext.Default.RuntimeLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));

            var assembly = (inCompileLibraries || inRuntimeLibraries)
                ? Assembly.Load(new AssemblyName(fileNameWithOutExtension))
                : AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFullPath);

            return assembly;
        }
    }
}
