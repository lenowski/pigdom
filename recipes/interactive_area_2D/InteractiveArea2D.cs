using Godot;

namespace Pigdom.Recipes;

public partial class InteractiveArea2D : Area2D
{
    [Signal]
    public delegate void InteractedEventHandler();

    [Signal]
    public delegate void InteractionAvailableEventHandler();

    [Signal]
    public delegate void InteractionUnavailableEventHandler();

    [Export]
    private string InteractInputAction { get; set; } = "interact";

    public override void _Ready()
    {
        SetProcessUnhandledInput(false);
        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
    }

    public override void _UnhandledInput(InputEvent evnt)
    {
        if (evnt.IsActionPressed(InteractInputAction))
        {
            EmitSignal(SignalName.Interacted);
            GetViewport().SetInputAsHandled();
        }
    }

    private void OnAreaEntered(Area2D area)
    {
        SetProcessUnhandledInput(true);
        EmitSignal(SignalName.InteractionAvailable);
    }

    private void OnAreaExited(Area2D area)
    {
        SetProcessUnhandledInput(false);
        EmitSignal(SignalName.InteractionUnavailable);
    }
}
