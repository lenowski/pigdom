using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class HitState : State
{
  public override async void Enter()
  {
    Context.AnimationTree.EnableCondition("hit");
    Context.Body.SetPhysicsProcess(false);

    await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);

    if (Context.Health > 0)
    {
      Context.TransitionTo(PreviousState);
    }
    else
    {
      Context.Score.IncreaseScore();
      Context.TransitionTo<DeadState>();
    }
  }

  public override void Exit()
  {
    Context.Body.SetPhysicsProcess(true);
    Context.AnimationTree.DisableCondition("hit");
  }
}
