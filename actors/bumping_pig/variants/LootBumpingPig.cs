using Pigdom.Objects;

namespace Pigdom.Actors;

public partial class LootBumpingPig : BumpingPig
{
    private Loot2D _loot;

    public override void _Ready()
    {
        _loot = GetNode<Loot2D>("BumpingEnemy2D/Loot2D");
        base._Ready();
    }

    protected override void Die()
    {
        _loot.Drop();
        base.Die();
    }
}
