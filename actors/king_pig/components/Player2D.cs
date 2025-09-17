using Godot;

namespace Pigdom.Actors.KingPig;

public partial class Player2D : Recipes.WallJumpingCharacter2D
{
    private const string MoveLeftAction = "move_left";
    private const string MoveRightAction = "move_right";
    private const string MoveDownAction = "move_down";
    private const string JumpAction = "jump";

    public override void _UnhandledInput(InputEvent evnt)
    {
        HandleHorizontalMovement(evnt);
        HandleVerticalMovement(evnt);
    }

    private void HandleHorizontalMovement(InputEvent evnt)
    {
        var newDirection = GetHorizontalDirection(evnt);
        if (newDirection.HasValue)
        {
            Direction = newDirection.Value;
        }
    }

    private int? GetHorizontalDirection(InputEvent evnt)
    {
        if (evnt.IsAction(MoveLeftAction))
        {
            return evnt.IsPressed() ? -1 : (Input.IsActionPressed(MoveRightAction) ? 1 : 0);
        }

        if (evnt.IsAction(MoveRightAction))
        {
            return evnt.IsPressed() ? 1 : (Input.IsActionPressed(MoveLeftAction) ? -1 : 0);
        }

        return null;
    }

    private void HandleVerticalMovement(InputEvent evnt)
    {
        if (evnt.IsActionPressed(JumpAction))
        {
            if (Input.IsActionPressed(MoveDownAction))
            {
                EnablePassThrough();
            }
            else
            {
                Jump();
            }
        }
        else if (evnt.IsActionReleased(JumpAction))
        {
            CancelJump();
        }

        if (evnt.IsActionReleased(MoveDownAction))
        {
            DisablePassThrough();
        }
    }
}
