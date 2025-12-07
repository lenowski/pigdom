using Godot;

namespace Pigdom.Recipes;

public partial class BumpingEnemy2D : BasicMovingCharacter2D
{
    [Signal]
    public delegate void BumpedEventHandler();

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (IsOnWall())
        {
            // Bump();
            EmitSignal(SignalName.Bumped);
        }
    }

    // private void Bump()
    // {
    //   Direction *= -1;
    // }
}
