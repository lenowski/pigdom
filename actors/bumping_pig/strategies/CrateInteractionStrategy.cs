using Godot;
using Pigdom.Actors.BumpingPig.States;

namespace Pigdom.Actors.BumpingPig.Strategies;

public partial class CrateInteractionStrategy : InteractionStrategy
{
    public override void Execute()
    {
        // Don't pick up if already carrying something
        // FIXME: This is a bit hacky, we should have a more explicit way to check if the actor is already carrying something
        if (Context.Actor.State is CarryIdleState or CarryRunState)
        {
            return;
        }

        Context.PickCommand.Item = InteractedArea.Owner as RigidBody2D;
        Context.Actor.PickingItemType = "crate";
        Context.Actor.ThrowableFactory.ProductPackedScene = GD.Load<PackedScene>(
            "res://objects/crate/variants/throw_crate/ThrowCrate.tscn"
        );
        Context.Actor.ThrowableFactory.TargetContainerName = "Crates";
        Context.PickCommand.Execute();
    }
}
