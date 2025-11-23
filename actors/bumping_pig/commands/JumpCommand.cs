using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class JumpCommand : Node, ICommand<BumpingPig>, ICommand
{
    public BumpingPig Receiver { get; set; }

    public void Execute()
    {
        Receiver?.Jump();
    }

    public void Undo()
    {
        Receiver?.CancelJump();
    }
}
