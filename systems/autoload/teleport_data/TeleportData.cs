using Godot;

namespace Pigdom.Objects;

public partial class TeleportData : Node
{
  public static TeleportData Instance { get; set; } = null!;

  public string TargetPortalName { get; set; } = "Door";

  public override void _Ready()
  {
    if (Instance is not null)
      GD.PrintErr("Multiple 'Score' instances detected!");
    Instance = this;
  }
}
