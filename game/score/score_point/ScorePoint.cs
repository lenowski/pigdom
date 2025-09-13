using Godot;
using Pigdom.Interface;

namespace Pigdom.Game;

public partial class ScorePoint : Node2D
{
    [Export]
    public int Amount { get; set; } = 100;

    private PackedScene _popLabelScene = GD.Load<PackedScene>("uid://2ifavtxuttds");
    private Node _popLabels;

    public override void _Ready()
    {
        _popLabels = FindParent("Level").FindChild("PopLabels");
    }

    public void IncreaseScore(int? score = null, bool popScore = true)
    {
        Score.Instance.Current += score ?? Amount;

        var game = FindParent("Game") as Game;

        if (game != null)
        {
            game.UpdateScoreLabel();
        }

        if (popScore)
        {
            PopLabel();
        }
    }

    private void PopLabel()
    {
        var popLabel = _popLabelScene.Instantiate<PopLabel>();
        _popLabels?.AddChild(popLabel);
        popLabel.Pop(Amount.ToString(), GlobalPosition);
    }
}
