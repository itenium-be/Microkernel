namespace Microkernel.Hook
{
    /// <summary>
    /// Context available in all Plugins.
    /// Could contain some global configuration, helpers for logging, ...
    /// </summary>
    public class Context
    {
        public IEventEmitter EventEmitter { get; }

        public Context(IEventEmitter em)
        {
            EventEmitter = em;
        }
    }
}
