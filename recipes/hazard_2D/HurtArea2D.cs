using Godot;

namespace Pigdom.Recipes;

public partial class HurtArea2D : Area2D
{
    [Signal]
    public delegate void HurtEventHandler(int damage);

    [Export]
    public int Defense { get; set; } = 0;

    [Export(PropertyHint.Enum, "Not Player,Player,Neutral")]
    public string Team { get; set; } = "Not Player";

    public void GetHurt(HitArea2D hitArea)
    {
        if (hitArea.Team != Team)
        {
            var finalDamage = Mathf.Max(0, hitArea.Damage - Defense);
            EmitSignal(SignalName.Hurt, finalDamage);
        }
    }
}
