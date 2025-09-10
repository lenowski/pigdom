using Godot;
using Pigdom.Recipes;

namespace Pigdom.Actors.CratePig;

public partial class CratePig : Node2D
{
    [Export]
    public int Health { get; private set; } = 3;

    private AnimationPlayer _animationPlayer;
    private HurtArea2D _hurtArea2D;
    private Area2D _visionArea2D;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _hurtArea2D = GetNode<HurtArea2D>("HurtArea2D");
        _visionArea2D = GetNode<Area2D>("VisionArea2D");

        _hurtArea2D.Hurt += OnHurtBox2DHurt;
        _visionArea2D.AreaEntered += OnVisionArea2DAreaEntered;
    }

    public void Jump() => _animationPlayer.Play("jump");

    private void OnVisionArea2DAreaEntered(Area2D area) => _animationPlayer.Play("look");

    private void OnCrateTreeExited() => QueueFree();

    private void OnHurtBox2DHurt(int damage)
    {
        Health -= damage;
        _animationPlayer.Play(Health < 1 ? "break" : "hit");
    }
}
