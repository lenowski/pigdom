using Godot;

public partial class BombInteractionStrategy : InteractionStrategy
{
    public override void Execute()
    {
        Context.PickCommand.Item = InteractedArea.Owner as Node2D;
        Context.Actor.PickingItemType = "bomb";
        Context.Actor.ThrowableFactory.ProductPackedScene = GD.Load<PackedScene>(
            "res://objects/bomb/Bomb.tscn"
        );
        Context.Actor.ThrowableFactory.TargetContainerName = "Bombs";
        Context.PickCommand.Execute();
    }
}
