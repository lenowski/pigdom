using Godot;

namespace Pigdom.Objects.Bomb;

public partial class Bomb : RigidBody2D
{
    private AnimationPlayer _animator;
    private Area2D _visionArea2D;
    private Area2D _visibleArea2D;

    public override void _Ready()
    {
        _animator = GetNode<AnimationPlayer>("AnimationPlayer");
        _visionArea2D = GetNode<Area2D>("VisionArea2D");
        _visibleArea2D = GetNode<Area2D>("VisibleArea2D");

        _visionArea2D.AreaEntered += OnVisionArea2DAreaEntered;
    }

    public void Explode()
    {
        _animator.Play("explode");
    }

    private void OnVisionArea2DAreaEntered(Area2D area)
    {
        if (area != _visibleArea2D)
        {
            Explode();
        }
    }
}
