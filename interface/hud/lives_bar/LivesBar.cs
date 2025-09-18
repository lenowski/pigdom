using Godot;

namespace Pigdom.Interface;

public partial class LivesBar : TextureRect
{
    private HBoxContainer _livesContainer;
    private int _lives;

    public override void _Ready()
    {
        _livesContainer = GetNode<HBoxContainer>("LivesBoxContainer");
        _lives = _livesContainer.GetChildCount();
    }

    private void HitHearts()
    {
        var heart = _livesContainer.GetChild<HeartTexture>(_lives - 1);
        heart.Hit();
        _lives -= 1;
    }

    private void RecoverHearts()
    {
        _lives += 1;
        var heart = _livesContainer.GetChild<HeartTexture>(_lives - 1);
        heart.Recover();
    }

    public void OnPlayerLivesIncreased(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RecoverHearts();
        }
    }

    public void OnPlayerLivesDecreased(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            HitHearts();
        }
    }
}
