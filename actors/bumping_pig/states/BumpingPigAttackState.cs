using Godot;

namespace Pigdom.Actors.States;

public partial class BumpingPigAttackState : BumpingPigState
{
    public override void Enter()
    {
        Context.EnableAnimation("attack");
        Context.DisablePhysics();
        Context.AnimationFinished += OnAnimationFinished;
    }

    public override void Exit()
    {
        Context.AnimationFinished -= OnAnimationFinished;
        Context.DisableAnimation("attack");
        Context.EnablePhysics();
    }

    public override void GetHurt()
    {
        Context.TransitionTo<BumpingPigHitState>();
    }

    private void OnAnimationFinished(StringName animationName)
    {
        if (animationName == "attack")
        {
            Context.AnimationFinished -= OnAnimationFinished;
            Context.TransitionTo(PreviousState);
        }
    }
}
