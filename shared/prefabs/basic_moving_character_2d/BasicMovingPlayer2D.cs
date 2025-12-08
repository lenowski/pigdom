using Godot;

namespace Pigdom.Recipes;

public partial class BasicMovingPlayer2D : BasicMovingCharacter2D
{
    private const string MoveLeftAction = "move_left";
    private const string MoveRightAction = "move_right";
    private const string JumpAction = "jump";

    public override void _UnhandledInput(InputEvent evnt)
    {
        HandleHorizontalMovement(evnt);
        HandleVerticalMovement(evnt);
    }

    private void HandleHorizontalMovement(InputEvent evnt)
    {
        if (evnt.IsAction(MoveLeftAction))
        {
            if (evnt.IsPressed())
            {
                Direction = -1; // Move left
            }
            else if (Input.IsActionPressed(MoveRightAction))
            {
                Direction = 1; // Left released, but right is still held
            }
            else
            {
                Direction = 0; // Stop moving
            }
        }
        else if (evnt.IsAction(MoveRightAction))
        {
            if (evnt.IsPressed())
            {
                Direction = 1; // Move right
            }
            else if (Input.IsActionPressed(MoveLeftAction))
            {
                Direction = -1; // Right released, but left is still held
            }
            else
            {
                Direction = 0; // Stop moving
            }
        }
    }

    private void HandleVerticalMovement(InputEvent evnt)
    {
        if (evnt.IsActionPressed(JumpAction))
        {
            Jump();
        }
        else if (evnt.IsActionReleased(JumpAction))
        {
            CancelJump();
        }
    }
}
