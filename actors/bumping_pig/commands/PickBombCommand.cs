using Godot;
using Pigdom.Objects.Bomb;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class PickBombCommand : Node, ICommand<BumpingPig>, ICommand
{
    public Bomb Bomb { get; set; }

    public BumpingPig Receiver { get; set; }

    public void Execute()
    {
        Receiver?.PickBomb(Bomb);
    }
}
