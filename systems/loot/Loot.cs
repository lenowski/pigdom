using Godot;
using Pigdom.Systems;

public partial class Loot : Marker2D
{
  [Export(PropertyHint.Range, "0.0,1.0,0.1")]
  public float DropRate { get; private set; }

  private Node2DFactory _factory;

  public override void _Ready()
  {
    _factory = GetNode<Node2DFactory>("Node2DFactory");
  }

  public void Drop()
  {
    var luck = GD.Randf();
    if (luck <= DropRate)
    {
      _factory.Create();
    }
  }
}
