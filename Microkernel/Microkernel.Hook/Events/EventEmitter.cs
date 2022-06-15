namespace Microkernel.Hook.Events
{
    public class EventEmitter : IEventEmitter
    {
        public async Task Emit(IInputEvent ev)
        {
            await ev.Processor(ev);
        }
    }
}
