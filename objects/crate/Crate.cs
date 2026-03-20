using Godot;
using Pigdom.Recipes;

namespace Pigdom.Objects;

public partial class Crate : RigidBody2D
{
    [Signal]
    public delegate void BrokeEventHandler();

    [Export]
    public int Health { get; set; } = 3;

    [Export]
    public float ShatterVelocityThreshold { get; set; } = 300f;

    private AnimationPlayer _animationPlayer;
    private HurtArea2D _hurtBox2D;
    private CollisionShape2D _collisionShape;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _hurtBox2D = GetNode<HurtArea2D>("HurtBox2D");
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        _hurtBox2D.Hurt += OnHurtBox2DHurt;
    }

    public override void _PhysicsProcess(double delta)
    {
        bool shouldFreeze = Sleeping;

        if (Freeze != shouldFreeze)
        {
            Freeze = shouldFreeze;
            _collisionShape.OneWayCollision = shouldFreeze;
        }
    }

    private void Damage(int damage)
    {
        Health -= damage;

        if (Health < 1)
        {
            CallDeferred(MethodName.Shatter);
        }
        else
        {
            CallDeferred(MethodName.PlayHitAnimation);
        }
    }

    protected virtual void Shatter()
    {
        WakeUpNearbyCrates();
        EmitSignal(SignalName.Broke);
        _animationPlayer.Play("break");
    }

    private void WakeUpNearbyCrates()
    {
        // Wake up any crates that are currently colliding with this one
        foreach (var body in GetCollidingBodies())
        {
            if (body is Crate crate)
            {
                crate.Sleeping = false;
            }
        }
    }

    private void PlayHitAnimation() => _animationPlayer.Play("hit");

    private void OnHurtBox2DHurt(int damage) => Damage(damage);
}
