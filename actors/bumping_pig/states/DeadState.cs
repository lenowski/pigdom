using System.Linq;
using Pigdom.Shared.Extensions;
using Pigdom.Systems;

namespace Pigdom.Actors.BumpingPig.States;

public partial class DeadState : State
{
    public override void Enter()
    {
        Context.AnimationTree.EnableCondition("dead");
        Context.Body.SetPhysicsProcess(false);
        foreach (var item in base.Context.Loots.GetChildren().OfType<Loot>())
        {
            item.Drop();
        }
    }
}
