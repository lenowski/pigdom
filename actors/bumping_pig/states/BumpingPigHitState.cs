using Godot;

namespace Pigdom.Actors.States;

public partial class BumpingPigHitState : BumpingPigState
{
    public override void Enter()
    {
        Context.EnableAnimation("hit");
        Context.DisablePhysics();
        Context.AnimationFinished += OnAnimationFinished;
    }

    public override void Exit()
    {
        Context.AnimationFinished -= OnAnimationFinished;
        Context.EnablePhysics();
        Context.DisableAnimation("hit");
    }

    private void OnAnimationFinished(StringName animationName)
    {
        if (animationName == "hit")
        {
            Context.AnimationFinished -= OnAnimationFinished;

            if (Context.CurrentHealth > 0)
            {
                Context.TransitionTo(PreviousState);
            }
            else
            {
                Context.IncreaseScore();
                Context.TransitionTo<BumpingPigDeadState>();
            }
        }
    }
}
