using Godot;
using Pigdom.Interface;
using Pigdom.Systems;

namespace Pigdom.Game;

public partial class ScorePoint : Node2D
{
  [Export]
  public int Amount { get; set; } = 100;

  private Node2DFactory _factory;
  private Node _popLabels;

  public override void _Ready()
  {
    _factory = GetNode<Node2DFactory>("Node2DFactory");
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
    if (_factory.Create() is PopLabel popLabel)
      popLabel.Pop(Amount.ToString(), GlobalPosition);
  }
}
