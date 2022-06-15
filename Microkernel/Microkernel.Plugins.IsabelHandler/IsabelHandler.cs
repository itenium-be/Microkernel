using Microkernel.Hook;
using Microkernel.Hook.BuildingBlocks;
using Microkernel.Hook.Events;

namespace Microkernel.Plugins.IsabelHandler
{
    public class IsabelHandler : FileMover
    {
        public override string InputPath => @"c:\temp\microkernel\isabel\in";
        public override string InputFilter => "*.xml";
        public override string OutputPath => @"c:\temp\microkernel\isabel\out";

        public IsabelHandler(Context context) : base(context) {}

        protected override Task ProcessCore(FileEvent ev)
        {
            // Do some validation of the file?
            // Create a hash check?
            return base.ProcessCore(ev);
        }
    }
}