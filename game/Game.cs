using Godot;
using Pigdom.Actors.KingPig;
using Pigdom.Interface;

namespace Pigdom.Game;

public partial class Game : Node
{
    private Label _scoreLabel;
    private KingPigPlayer2D _player;
    private LivesTextureRect _livesInterface;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("InterfaceCanvasLayer/ScoreLabel");
        _player = GetNode<KingPigPlayer2D>("WorldCanvasLayer/Level/PlayerCharacter2D");
        _livesInterface = GetNode<LivesTextureRect>("InterfaceCanvasLayer/LivesTextureRect");

        UpdateScoreLabel();
        SetupLivesInterface();
    }

    public void UpdateScoreLabel()
    {
        _scoreLabel.Text = $"{Score.Instance.Current}";
    }

    private void SetupLivesInterface()
    {
        _player.LivesDecreased += _livesInterface.OnPlayerLivesDecreased;
        _player.LivesIncreased += _livesInterface.OnPlayerLivesIncreased;
    }
}
