using Godot;

namespace Pigdom.Recipes;

public partial class WallDetectorRayCast2D : RayCast2D
{
    [Signal]
    public delegate void WallEnteredEventHandler();

    [Signal]
    public delegate void WallExitedEventHandler();

    public bool WallColliding { get; set; } = false;

    public override void _PhysicsProcess(double delta)
    {
        if (IsColliding())
        {
            if (!WallColliding)
            {
                EmitSignal(SignalName.WallEntered);
            }
            WallColliding = true;
        }
        else
        {
            if (WallColliding)
            {
                EmitSignal(SignalName.WallExited);
                WallColliding = false;
            }
        }
    }
}
