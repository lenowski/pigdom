using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class MoveCommand : Node, ICommand<BumpingPig>, ICommand
{
    [Export]
    public int Direction { get; set; }

    public BumpingPig Receiver { get; set; }

    public void Execute()
    {
        Receiver?.Move(Direction);
    }
}
