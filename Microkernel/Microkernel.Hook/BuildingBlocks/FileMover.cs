using Microkernel.Hook.Events;

namespace Microkernel.Hook.BuildingBlocks
{
    public abstract class FileMover : IHook
    {
        private readonly FileSystemWatcher _watcher;
        protected readonly Context _context;

        public abstract string InputPath { get; }
        public abstract string InputFilter { get; }
        public abstract string OutputPath { get; }

        public FileMover(Context context)
        {
            _watcher = new FileSystemWatcher();
            _context = context;
        }

        public Task<bool> SetupListener()
        {
            _watcher.Path = InputPath;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Filter = InputFilter;
            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.EnableRaisingEvents = true;
            return Task.FromResult(true);
        }

        private void OnChanged(object sender, FileSystemEventArgs args)
        {
            Console.WriteLine("File found!");
            var ev = new FileEvent(args.FullPath, Process);
            _context.EventEmitter.Emit(ev);
        }

        public async Task<bool> Process(IInputEvent ev)
        {
            try
            {
                var fileEvent = (FileEvent)ev;
                await ProcessCore(fileEvent);

                var file = new FileInfo(fileEvent.FullPath);
                File.Move(fileEvent.FullPath, Path.Combine(OutputPath, file.Name));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing file" + ex);
                return false;
            }
            return true;
        }

        protected virtual Task ProcessCore(FileEvent ev)
        {
            return Task.CompletedTask;
        }
    }
}
