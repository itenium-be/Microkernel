using Microkernel;

Console.WriteLine("Starting MGT-X");

var plugins = new Plugins();
await plugins.Setup();

Console.WriteLine("Waiting for input");
SpinWait.SpinUntil(() => false);
