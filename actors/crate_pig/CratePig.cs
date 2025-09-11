using Godot;
using Pigdom.Game;
using Pigdom.Recipes;

namespace Pigdom.Actors;

public partial class CratePig : Node2D
{
    [Export]
    public int Health { get; set; } = 3;

    private AnimationPlayer _animationPlayer;
    private ScorePoint _scorePoint;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _scorePoint = GetNode<ScorePoint>("ScorePoint");

        var hurtArea2D = GetNode<HurtArea2D>("HurtBox2D");
        var visionArea2D = GetNode<Area2D>("VisionArea2D");

        hurtArea2D.Hurt += OnHurtBox2DHurt;
        visionArea2D.AreaEntered += OnVisionArea2DAreaEntered;
    }

    public void PlayJumpAnimation() => _animationPlayer.Play("jump");

    private void PlayBreakAnimation() => _animationPlayer.Play("break");

    private void PlayHitAnimation() => _animationPlayer.Play("hit");

    private void OnVisionArea2DAreaEntered(Area2D area) => _animationPlayer.Play("look");

    private void OnCrateTreeExited() => QueueFree();

    private void OnHurtBox2DHurt(int damage)
    {
        Health -= damage;
        if (Health < 1)
        {
            CallDeferred(MethodName.PlayBreakAnimation);
            _scorePoint.IncreaseScore();
        }
        else
        {
            CallDeferred(MethodName.PlayHitAnimation);
        }
    }
}
