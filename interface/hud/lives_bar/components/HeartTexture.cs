using Godot;

namespace Pigdom.Interface;

public partial class HeartTexture : TextureRect
{
  private AnimationPlayer _animationPlayer;

  public override void _Ready()
  {
    _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
  }

  public void Hit() => _animationPlayer.Play("hit");

  public void Recover() => _animationPlayer.Play("recover");
}
