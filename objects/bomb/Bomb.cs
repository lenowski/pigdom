using Godot;

namespace Pigdom.Objects.Bomb;

public partial class Bomb : RigidBody2D
{
    private AnimationPlayer animator;
    private Area2D visionArea2D;

    public override void _Ready()
    {
        animator = GetNode<AnimationPlayer>("AnimationPlayer");
        visionArea2D = GetNode<Area2D>("VisionArea2D");

        visionArea2D.AreaEntered += OnVisionArea2DAreaEntered;
    }

    public void Explode()
    {
        animator.Play("explode");
        Sleeping = true;
    }

    private void OnVisionArea2DAreaEntered(Area2D area) => Explode();
}
