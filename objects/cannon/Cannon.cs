using Godot;
using Pigdom.Systems;

namespace Pigdom.Objects;

public partial class Cannon : Node2D
{
    [Export]
    public float ShootingSpeed { get; set; } = -1200f;

    private Node2DFactory _cannonBallFactory;
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _cannonBallFactory = GetNode<Node2DFactory>("CannonBallFactory");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void Fire()
    {
        var ball = _cannonBallFactory.Create() as RigidBody2D;
        var shootingVelocity = new Vector2(ShootingSpeed, 0f);

        ball?.ApplyImpulse(shootingVelocity);
    }

    public void Shoot()
    {
        _animationPlayer.Play("shoot");
    }
}
