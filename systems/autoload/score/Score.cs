using Godot;

namespace Pigdom.Game;

public partial class Score : Node
{
    private int _current;
    private int _high;

    public static Score Instance { get; private set; } = null!;

    public int Current
    {
        get => _current;
        set
        {
            if (value < 0)
            {
                GD.PrintErr($"Score cannot be negative: {value}");
                return;
            }

            _current = value;
            if (value > _high)
            {
                _high = value;
            }
        }
    }

    public int High => _high;

    public override void _Ready()
    {
        if (Instance is not null)
            GD.PrintErr("Multiple 'Score' instances detected!");
        Instance = this;
    }

    public void Reset(bool resetHighScore = false)
    {
        _current = 0;
        if (resetHighScore)
            _high = 0;
    }
}
