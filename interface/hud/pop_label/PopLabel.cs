using Godot;

namespace Pigdom.Interface;

public partial class PopLabel : Node2D
{
  private AnimationPlayer _animationPlayer;
  private Label _label;

  public override void _Ready()
  {
    _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    _label = GetNode<Label>("PivotMarker2D/Label");
  }

  public void Pop(string text, Vector2 popPosition)
  {
    GlobalPosition = popPosition;
    _label.Text = text;
    _animationPlayer.Play("pop");
  }
}
