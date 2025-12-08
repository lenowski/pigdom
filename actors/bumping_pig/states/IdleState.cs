using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class IdleState : State
{
    public override void Enter()
    {
        Context.AnimationTree.EnableCondition("idle");
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition("idle");
    }

    public override void Move(int direction)
    {
        if (direction != 0)
        {
            Turn(direction);
            Context.Body.Direction = direction;
            Context.TransitionTo<RunState>();
        }
    }

    public override void Jump()
    {
        Context.Body.Jump();
        Context.TransitionTo<JumpState>();
    }

    public override void Attack()
    {
        Context.TransitionTo<AttackState>();
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }

    public override void Turn(int direction)
    {
        var baseScale = Context.Sprites.Scale;

        Context.Sprites.Scale = direction switch
        {
            > 0 => baseScale with { X = -1 },
            < 0 => baseScale with { X = 1 },
            _ => baseScale,
        };
    }
}
