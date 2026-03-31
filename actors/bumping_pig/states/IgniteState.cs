using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class IgniteState : State
{
    private Timer _igniteTimer;
    private int _previousDirection = 0;

    public override void _Ready()
    {
        _igniteTimer = GetNode<Timer>("IgniteTimer");
        _igniteTimer.Timeout += OnLightOnTimerTimeout;
    }

    public override async void Enter()
    {
        _previousDirection = Context.Body.Direction;
        Context.Body.Direction = 0;
        Context.AnimationTree.EnableCondition("light_match");
        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);
        Context.AnimationTree.SetConditionPath("parameters/IgniteStateMachine/conditions");
        _igniteTimer.Start();
    }

    public override void Exit()
    {
        Context.AnimationTree.SetConditionPath("parameters/conditions");
        Context.AnimationTree.DisableCondition("light_match");
        Context.Body.Direction = _previousDirection;
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }

    public async void OnLightOnTimerTimeout()
    {
        Context.AnimationTree.EnableCondition("light_cannon");
        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);
        Context.Cannon.Shoot();
        Context.AnimationTree.DisableCondition("light_cannon");
        Context.TransitionTo(PreviousState);
    }
}
