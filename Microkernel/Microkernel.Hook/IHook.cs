using Microkernel.Hook.Events;

namespace Microkernel.Hook
{
    /// <summary>
    /// Implement this interface to plugin your functionality in MGT-X
    /// </summary>
    public interface IHook
    {
        Task<bool> SetupListener();
        Task<bool> Process(IInputEvent ev);
    }
}