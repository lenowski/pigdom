using Godot;
using Pigdom.Shared.Prefabs;

namespace Pigdom.Objects;

public partial class CannonBall : RigidBody2D
{
    private AnimationPlayer _animationPlayer;
    private Timer _vanishTimer;
    private HitArea2D _hitArea;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _vanishTimer = GetNode<Timer>("VanishTimer");
        _hitArea = GetNode<HitArea2D>("HitArea2D");

        BodyEntered += OnBodyEntered;
        _hitArea.HitLanded += OnHitArea2DHitLanded;
        _vanishTimer.Timeout += OnVanishTimerTimeout;
    }

    private void OnBodyEntered(Node body)
    {
        if (_vanishTimer.IsStopped())
        {
            _vanishTimer.Start();
        }
    }

    private void OnVanishTimerTimeout()
    {
        _animationPlayer.Play("vanish");
    }

    private void OnHitArea2DHitLanded(int damage)
    {
        QueueFree();
    }
}
