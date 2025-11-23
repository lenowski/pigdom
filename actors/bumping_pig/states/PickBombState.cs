using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class PickBombState : State
{
    public override async void Enter()
    {
        Context.AnimationTree.EnableCondition("pick_bomb");
        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);
        Context.TransitionTo<RunBombState>();
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition("pick_bomb");
        Context.AnimationTree.SetConditionPath("parameters/StateMachine/conditions");
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }
}
