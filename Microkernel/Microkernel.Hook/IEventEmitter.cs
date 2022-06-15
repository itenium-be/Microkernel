using Microkernel.Hook.Events;

namespace Microkernel.Hook
{
    /// <summary>
    /// Once input has been found, it is not processed directly.
    /// Instead an event is launched so that it can be processed asynchronously.
    /// </summary>
    public interface IEventEmitter
    {
        Task Emit(IInputEvent ev);
    }
}
