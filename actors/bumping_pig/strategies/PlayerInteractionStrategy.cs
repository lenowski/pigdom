using Pigdom.Actors.BumpingPig.States;

public partial class PlayerInteractionStrategy : InteractionStrategy
{
    public override void Execute()
    {
        var currentState = Context.Actor.State;

        if (currentState is (CarryIdleState or CarryRunState))
        {
            Context.ThrowCommand.Execute();
        }
        else
        {
            Context.AttackCommand.Execute();
        }
    }
}
