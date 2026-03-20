using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public partial class ThrowCommand : Node, ICommand<BumpingPig>, ICommand
{
    [Export]
    public Vector2 Impulse { get; set; } = new(600, -600);

    public BumpingPig Receiver { get; set; }

    public Vector2 ThrowForce
    {
        get { return _throwForce ?? Impulse; }
        set { _throwForce = value; }
    }
    private Vector2? _throwForce;

    public void Execute()
    {
        Receiver?.Throw(ThrowForce);
    }
}
