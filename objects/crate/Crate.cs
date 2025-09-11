using Godot;
using Pigdom.Recipes;

namespace Pigdom.Objects;

public partial class Crate : Node2D
{
    [Signal]
    public delegate void BrokeEventHandler();

    [Export]
    public int Health { get; set; } = 3;

    [Export]
    public float Gravity { get; set; } = 4000f;

    private AnimationPlayer _animationPlayer;
    private CharacterBody2D _body;
    private RayCast2D _ray;
    private HurtArea2D _hurtBox2D;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _body = GetNode<CharacterBody2D>("CharacterBody2D");
        _ray = GetNode<RayCast2D>("CharacterBody2D/RayCast2D");
        _hurtBox2D = GetNode<HurtArea2D>("CharacterBody2D/HurtBox2D");

        _hurtBox2D.Hurt += OnHurtBox2DHurt;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_ray.IsColliding())
        {
            _body.Velocity = _body.Velocity with { Y = _body.Velocity.Y + Gravity * (float)delta };
        }

        _body.MoveAndSlide();
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
        EmitSignal(SignalName.Broke);
        _animationPlayer.Play("break");
    }

    private void PlayHitAnimation() => _animationPlayer.Play("hit");

    private void OnHurtBox2DHurt(int damage) => Damage(damage);
}
