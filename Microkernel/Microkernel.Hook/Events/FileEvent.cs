namespace Microkernel.Hook.Events
{
    public class FileEvent : IInputEvent
    {
        public string FullPath { get; }

        public Func<IInputEvent, Task<bool>> Processor { get; }

        public FileEvent(string fullPath, Func<IInputEvent, Task<bool>> processor)
        {
            FullPath = fullPath;
            Processor = processor;
        }

        public string GetContent()
        {
            return File.ReadAllText(FullPath);
        }
    }
}
