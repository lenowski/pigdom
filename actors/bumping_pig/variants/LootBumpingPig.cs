using System.Linq;
using Godot;

namespace Pigdom.Actors;

public partial class LootBumpingPig : BumpingPig
{
    private Node2D _loots;

    public override void _Ready()
    {
        _loots = GetNode<Node2D>("Loots");
        base._Ready();
    }

    protected override void Die()
    {
        foreach (var loot in _loots.GetChildren().OfType<Loot>())
        {
            loot.Drop();
        }
        base.Die();
    }
}
