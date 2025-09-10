namespace Pigdom.Recipes;

public partial class BumpingEnemy2D : BasicMovingCharacter2D
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (IsOnWall())
        {
            Bump();
        }
    }

    private void Bump() => Direction *= -1;
}
