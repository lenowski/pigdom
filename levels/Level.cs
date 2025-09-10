using Godot;
using Pigdom.Actors.KingPig;

namespace Pigdom.Levels;

public partial class Level : Node2D
{
    private AnimationPlayer _fade;
    private KingPigPlayer2D _playerCharacter2D;

    public override void _Ready()
    {
        _playerCharacter2D = GetNode<KingPigPlayer2D>("PlayerCharacter2D");
        _fade = GetNode<AnimationPlayer>("CanvasLayer/ColorRect/AnimationPlayer");

        _fade.Play("fade_in");

        _playerCharacter2D.Died += OnPlayerCharacter2DDied;
    }

    private void OnPlayerCharacter2DDied()
    {
        GetTree().ReloadCurrentScene();
    }
}
