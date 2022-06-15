namespace Microkernel.Hook.Events
{
    public interface IInputEvent
    {
        Func<IInputEvent, Task<bool>> Processor { get; }
        string GetContent();
    }
}
