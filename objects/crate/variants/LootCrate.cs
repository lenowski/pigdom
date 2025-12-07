using System.Linq;
using Godot;

namespace Pigdom.Objects;

public partial class LootCrate : Crate
{
  private Node2D _loots;

  public override void _Ready()
  {
    _loots = GetNode<Node2D>("Loots");

    base._Ready();
  }

  protected override void Shatter()
  {
    base.Shatter();
    foreach (var loot in _loots.GetChildren().OfType<Loot>())
    {
      loot.Drop();
    }
  }
}
