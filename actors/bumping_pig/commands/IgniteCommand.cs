using Godot;
using Pigdom.Objects;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class IgniteCommand : Node, ICommand<BumpingPig>, ICommand
{
    public Cannon Cannon { get; set; }

    public BumpingPig Receiver { get; set; }

    public void Execute()
    {
        Receiver?.Ignite(Cannon);
    }
}
