using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class AttackCommand : Node, ICommand<BumpingPig>, ICommand
{
  public BumpingPig Receiver { get; set; }

  public void Execute()
  {
    Receiver?.Attack();
  }
}
