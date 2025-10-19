namespace Pigdom.Actors.States;

public partial class BumpingPigDeadState : BumpingPigState
{
    public override void Enter()
    {
        Context.DisablePhysics();
        Context.DropLoot();
        Context.EnableAnimation("dead");
    }
}
