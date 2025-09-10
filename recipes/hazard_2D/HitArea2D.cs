using Godot;

namespace Pigdom.Recipes;

public partial class HitArea2D : Area2D
{
    [Signal]
    public delegate void HitLandedEventHandler(int damage);

    [Export]
    public int Damage { get; private set; } = 1;

    [Export(PropertyHint.Enum, "Not Player,Player,Neutral")]
    public string Team { get; private set; } = "Not Player";

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void Hit(HurtArea2D hurtArea)
    {
        if (hurtArea.Team != Team)
        {
            var finalDamage = Mathf.Max(0, Damage - hurtArea.Defense);
            EmitSignal(SignalName.HitLanded, finalDamage);
            hurtArea.GetHurt(this);
        }
    }

    private void OnAreaEntered(Area2D area2D)
    {
        if (area2D is HurtArea2D hurtArea)
        {
            Hit(hurtArea);
        }
    }
}
