using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class PickCommand : Node, ICommand<BumpingPig>, ICommand
{
    public Node2D Item { get; set; }

    public BumpingPig Receiver { get; set; }

    public void Execute()
    {
        Receiver?.Pick(Item);
    }
}
