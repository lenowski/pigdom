using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class ThrowBombState : State
{
    private int _previousDirection = 1;

    public override async void Enter()
    {
        _previousDirection = Context.Body.Direction;
        Context.Body.Direction = 0;
        Context.AnimationTree.EnableCondition("throw_bomb");

        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);

        if (_previousDirection == 0)
        {
            Context.TransitionTo<IdleState>();
        }
        else
        {
            Context.TransitionTo<RunState>();
        }
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition("throw_bomb");
        Context.AnimationTree.SetConditionPath("parameters/conditions");
        Context.Body.Direction = _previousDirection;
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }
}
