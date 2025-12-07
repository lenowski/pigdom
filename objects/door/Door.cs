using Godot;
using Pigdom.Objects;

public partial class Door : Node2D
{
  [Export(PropertyHint.File, "*.tscn")]
  private string TargetScene { get; set; }

  [Export]
  public string TargetDoor = "Door";

  [Signal]
  public delegate void OpenedEventHandler();

  [Signal]
  public delegate void ClosedEventHandler();

  private AnimationPlayer _animationPlayer;

  public override void _Ready()
  {
    _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    var area2D = GetNode<Area2D>("Area2D");

    area2D.AreaEntered += OnArea2DAreaEntered;
    area2D.AreaExited += OnArea2DAreaExited;

    SetProcessUnhandledInput(false);
  }

  public override void _UnhandledInput(InputEvent evnt)
  {
    if (evnt.IsActionPressed("interact"))
    {
      LoadNextSceneAsync();
    }
  }

  public async void LoadNextSceneAsync(string nextScene = null)
  {
    nextScene ??= TargetScene;

    TeleportData.Instance.TargetPortalName = TargetDoor;

    _animationPlayer.Play("open");
    EmitSignal(SignalName.Opened);
    await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

    _animationPlayer.Play("close");
    await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
    EmitSignal(SignalName.Closed);

    GetTree().ChangeSceneToFile(nextScene);
  }

  private void OnArea2DAreaEntered(Area2D area)
  {
    SetProcessUnhandledInput(true);
  }

  private void OnArea2DAreaExited(Area2D area)
  {
    SetProcessUnhandledInput(false);
  }
}
