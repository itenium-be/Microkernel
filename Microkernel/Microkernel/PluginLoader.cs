using Microkernel.Hook;
using System.Runtime.Loader;

namespace Microkernel
{
    internal class PluginLoader
    {
        private const string PluginPath = @"c:\temp\microkernel\plugins";

        public static IEnumerable<Type> GetPlugins()
        {
            var files = Directory.GetFiles(PluginPath, "*.dll");
            var plugins = new List<Type>();
            foreach (var file in files)
            {
                try
                {
                    var hooksType = typeof(IHook);
                    var myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                    var hooks = myAssembly.GetTypes()
                        .Where(x => hooksType.IsAssignableFrom(x))
                        .ToArray();

                    plugins.AddRange(hooks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("failed to load {0}", file);
                    Console.WriteLine(ex.ToString());
                }
            }
            return plugins;
        }
    }
}
