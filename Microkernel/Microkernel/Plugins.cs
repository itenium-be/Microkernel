using Microkernel.Hook;
using Microkernel.Hook.Events;

namespace Microkernel
{
    internal class Plugins
    {
        private readonly Context _context = new(new EventEmitter());

        public async Task Setup()
        {
            var plugins = PluginLoader.GetPlugins();
            foreach (var plugin in plugins)
            {
                Console.WriteLine($"Setting up: {plugin}");
                var instance = (IHook)Activator.CreateInstance(plugin, _context);
                await instance.SetupListener();
            }
        }
    }
}
