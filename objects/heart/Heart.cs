using Godot;
using Pigdom.Actors.KingPig;
using Pigdom.Recipes;

namespace KingsAndPigs.Objects;

public partial class Heart : Node2D
{
  [Export]
  public int HealAmount { get; private set; } = 1;

  private InteractiveArea2D _interactiveArea2D;

  public override void _Ready()
  {
    _interactiveArea2D = GetNode<InteractiveArea2D>("InteractiveArea2D");
    _interactiveArea2D.AreaEntered += OnInteractiveArea2DAreaEntered;
  }

  public void Heal(KingPigPlayer2D character)
  {
    character.Lives += HealAmount;
  }

  private void OnInteractiveArea2DAreaEntered(Area2D area)
  {
    var player = area.GetParentOrNull<KingPigPlayer2D>();
    if (player != null)
    {
      Heal(player);
      QueueFree();
    }
  }
}
