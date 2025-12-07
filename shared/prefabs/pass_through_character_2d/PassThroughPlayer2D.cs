using Godot;

namespace Pigdom.Recipes;

public partial class PassThroughPlayer2D : PassThroughCharacter2D
{
  private const string MoveLeftAction = "move_left";
  private const string MoveRightAction = "move_right";
  private const string MoveDownAction = "move_down";
  private const string JumpAction = "jump";

  public override void _UnhandledInput(InputEvent inputEvent)
  {
    HandleHorizontalMovement(inputEvent);
    HandleVerticalMovement(inputEvent);
    HandlePassThroughRelease(inputEvent);
  }

  private void HandleHorizontalMovement(InputEvent inputEvent)
  {
    if (inputEvent.IsAction(MoveLeftAction))
    {
      if (inputEvent.IsPressed())
      {
        Direction = -1;
      }
      else if (Input.IsActionPressed(MoveRightAction))
      {
        Direction = 1;
      }
      else
      {
        Direction = 0;
      }
    }
    else if (inputEvent.IsAction(MoveRightAction))
    {
      if (inputEvent.IsPressed())
      {
        Direction = 1;
      }
      else if (Input.IsActionPressed(MoveLeftAction))
      {
        Direction = -1;
      }
      else
      {
        Direction = 0;
      }
    }
  }

  private void HandleVerticalMovement(InputEvent inputEvent)
  {
    if (inputEvent.IsActionPressed(JumpAction))
    {
      // Pass through logic: if down is pressed while jumping, enable pass through
      if (Input.IsActionPressed(MoveDownAction))
      {
        EnablePassThrough();
      }
      else
      {
        Jump();
      }
    }
    else if (inputEvent.IsActionReleased(JumpAction))
    {
      CancelJump();
    }
  }

  private void HandlePassThroughRelease(InputEvent inputEvent)
  {
    if (inputEvent.IsActionReleased(MoveDownAction))
    {
      DisablePassThrough();
    }
  }
}
