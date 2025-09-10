using Pigdom.Objects;

public partial class LootCrate : Crate
{
    private Loot2D _loot;

    public override void _Ready()
    {
        _loot = GetNode<Loot2D>("CharacterBody2D/Loot2D");
        base._Ready();
    }

    protected override void Shatter()
    {
        _loot.Drop();
        base.Shatter();
    }
}
