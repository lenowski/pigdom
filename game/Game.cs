using Godot;

namespace Pigdom.Game;

public partial class Game : Node
{
    private Label _scoreLabel;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("InterfaceCanvasLayer/ScoreLabel");
        UpdateScoreLabel();
    }

    public void UpdateScoreLabel()
    {
        _scoreLabel.Text = $"{Score.Instance.Current}";
    }
}
