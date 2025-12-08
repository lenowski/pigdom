using Godot;
using Pigdom.Actors.KingPig;
using Pigdom.Objects;

namespace Pigdom.Levels;

public partial class Level : Node2D
{
    private AnimationPlayer _fade;
    private KingPigPlayer2D _player;

    public override void _Ready()
    {
        _fade = GetNode<AnimationPlayer>("CanvasLayer/ColorRect/AnimationPlayer");
        _player = GetNode<KingPigPlayer2D>("PlayerCharacter2D");

        _fade.Play("fade_in");

        _player.GlobalPosition = (
            FindChild(TeleportData.Instance.TargetPortalName) as Node2D
        ).GlobalPosition;
        _player.FadeIn();

        _player.Died += OnPlayerDied;
    }

    private void OnPlayerDied() => GetTree().ReloadCurrentScene();

    private void OnDoorOpened()
    {
        _player.FadeOut();
        _fade.Play("fade_out");
    }
}
