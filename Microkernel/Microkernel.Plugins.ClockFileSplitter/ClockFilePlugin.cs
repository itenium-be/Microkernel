using Microkernel.Hook;
using Microkernel.Hook.Events;

namespace Microkernel.Plugins.ClockFileSplitter
{
    /// <summary>
    /// Split the clockfile in smaller files for the legacy application to handle.
    /// The file has become so big that the legacy app crashes with an OutOfMemoryException.
    /// </summary>
    public class ClockFilePlugin : IHook
    {
        private const string InputPath = @"c:\temp\microkernel\clockfile";
        private readonly FileSystemWatcher _watcher;
        private readonly Context _context;

        public ClockFilePlugin(Context context)
        {
            _watcher = new FileSystemWatcher();
            _context = context;
        }

        public Task<bool> SetupListener()
        {
            _watcher.Path = InputPath;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Filter = "clock-in.txt";
            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.EnableRaisingEvents = true;
            return Task.FromResult(true);
        }

        private void OnChanged(object sender, FileSystemEventArgs args)
        {
            Console.WriteLine("ClockFile found!");
            var ev = new FileEvent(args.FullPath, Process);
            _context.EventEmitter.Emit(ev);
        }

        public Task<bool> Process(IInputEvent ev)
        {
            var content = ev.GetContent();

            var outputPath = Path.Combine(InputPath, "output");
            File.WriteAllText(Path.Combine(outputPath, "clock1.txt"), content.Substring(0, 1000));
            File.WriteAllText(Path.Combine(outputPath, "clock2.txt"), content.Substring(1000, 2000));

            Console.WriteLine("ClockFile processed!");
            return Task.FromResult(true);
        }
    }
}