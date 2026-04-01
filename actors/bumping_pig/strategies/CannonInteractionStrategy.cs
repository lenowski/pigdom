using Pigdom.Objects;

namespace Pigdom.Actors.BumpingPig.Strategies;

public partial class CannonInteractionStrategy : InteractionStrategy
{
    public override void Execute()
    {
        Context.IgniteCommand.Cannon = InteractedArea.GetParent<Cannon>();
        Context.IgniteCommand.Execute();
    }
}
