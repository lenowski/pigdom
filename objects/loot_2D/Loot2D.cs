using Godot;

namespace Pigdom.Objects;

public partial class Loot2D : Marker2D
{
    [Export]
    public PackedScene LootScene { get; set; } = GD.Load<PackedScene>("uid://c80fci1r5e5lj");

    [Export(PropertyHint.Range, "0.0, 1.0, ")]
    public float DropRate { get; set; } = .3f;

    public void Drop()
    {
        var luck = GD.Randf();
        GD.Print(luck);

        if (luck <= DropRate)
        {
            var loot = LootScene.Instantiate<Node2D>();
            loot.GlobalPosition = GlobalPosition;
            FindParent("Level").FindChild("Diamonds").AddChild(loot);
        }
    }
}
