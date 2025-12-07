using Godot;

namespace Pigdom.Systems;

public partial class Node2DFactory : Marker2D
{
  [Signal]
  public delegate void CreatedEventHandler(Node2D product);

  [Export]
  public PackedScene ProductPackedScene { get; set; }

  [Export]
  public string TargetContainerName { get; set; }

  public Node2D Create(PackedScene productPackedScene = null)
  {
    productPackedScene ??= ProductPackedScene;

    if (productPackedScene == null)
    {
      GD.PrintErr(
          "ProductPackedScene property is not assigned. Please configure it in the Godot editor."
      );
      return null;
    }

    var product = productPackedScene.Instantiate<Node2D>();
    product.GlobalPosition = GlobalPosition;

    var container = FindParent("Level").FindChild(TargetContainerName);
    if (container == null)
    {
      GD.PrintErr(
          $"Target container '{TargetContainerName}' could not be found in the scene hierarchy."
      );
      return null;
    }
    container.AddChild(product);

    EmitSignal(SignalName.Created, product);

    return product;
  }
}
