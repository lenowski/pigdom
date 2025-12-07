using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class TurnCommand : Node, ICommand<BumpingPig>, ICommand
{
  [Export]
  public int Direction { get; set; }

  public BumpingPig Receiver { get; set; }

  public void Execute()
  {
    Receiver?.Turn(Direction);
  }
}
