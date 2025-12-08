using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class StopCommand : Node, ICommand<BumpingPig>, ICommand
{
    public BumpingPig Receiver { get; set; }

    public void Execute()
    {
        Receiver?.Stop();
    }
}
