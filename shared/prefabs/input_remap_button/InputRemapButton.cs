using System.Linq;
using Godot;

namespace Pigdom.Recipes;

public partial class InputRemapButton : Button
{
  [Export]
  private string BindingAction { get; set; } = "attack";

  public override void _Ready()
  {
    SetProcessInput(false);

    var events = InputMap.ActionGetEvents(BindingAction);
    var keyEvent = events.OfType<InputEventKey>().FirstOrDefault();
    if (keyEvent != null)
    {
      Text = keyEvent.AsTextPhysicalKeycode();
    }
  }

  public override void _Toggled(bool toggledOn)
  {
    if (toggledOn)
    {
      ReleaseFocus();
      SetProcessInput(true);
    }
    else
    {
      SetProcessInput(false);
      GrabFocus();
    }
  }

  public override void _Input(InputEvent evnt)
  {
    if (evnt is InputEventKey keyEvent)
    {
      InputMap.ActionEraseEvents(BindingAction);
      InputMap.ActionAddEvent(BindingAction, evnt);
      Text = keyEvent.AsTextPhysicalKeycode();
      ButtonPressed = false;
    }
  }
}
