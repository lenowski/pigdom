using Godot;

namespace Pigdom.Objects;

public partial class Loot2D : Marker2D
{
    [Export]
    private PackedScene _lootScene = GD.Load<PackedScene>("res://objects/diamond/Diamond.tscn");

    [Export(PropertyHint.Range, "0.0, 1.0, ")]
    private float _dropRate = .3f;

    public void Drop()
    {
        var luck = GD.Randf();
        GD.Print(luck);

        if (luck <= _dropRate)
        {
            var loot = _lootScene.Instantiate<Node2D>();
            loot.GlobalPosition = GlobalPosition;
            FindParent("Level").FindChild("Diamonds").AddChild(loot);
        }
    }
}
