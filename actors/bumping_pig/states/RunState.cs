using Pigdom.Extensions;
using Pigdom.Objects.Bomb;

namespace Pigdom.Actors.BumpingPig.States;

public partial class RunState : State
{
    public override void Enter()
    {
        Context.AnimationTree.EnableCondition("run");
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition("run");
    }

    public override void Move(int direction)
    {
        if (direction != 0)
        {
            Turn(direction);
            Context.Body.Direction = direction;
        }
    }

    public override void Stop()
    {
        Context.Body.Direction = 0;
        Context.TransitionTo<IdleState>();
    }

    public override void Jump()
    {
        Context.Body.Jump();
        Context.TransitionTo<JumpState>();
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

    public override void Attack()
    {
        Context.TransitionTo<AttackState>();
    }

    public override void PickBomb(Bomb bomb)
    {
        bomb.QueueFree();
        Context.TransitionTo<PickBombState>();
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }
}
