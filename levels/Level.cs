using Godot;
using Pigdom.Actors.KingPig;

namespace Pigdom.Levels;

public partial class Level : Node2D
{
    public override void _Ready()
    {
        var playerCharacter2D = GetNode<KingPigPlayer2D>("PlayerCharacter2D");
        var fade = GetNode<AnimationPlayer>("CanvasLayer/ColorRect/AnimationPlayer");

        fade.Play("fade_in");

        playerCharacter2D.Died += OnPlayerCharacter2DDied;
    }

    private void OnPlayerCharacter2DDied() => GetTree().ReloadCurrentScene();
}
