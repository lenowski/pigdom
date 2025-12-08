using Godot;
using Pigdom.Actors.BumpingPig.Commands;

namespace Pigdom.Actors.BumpingPig.Debug;

public partial class CommandButton : Button
{
    private ICommand _command;

    [Export(PropertyHint.NodeType, "Command")]
    public NodePath CommandPath { get; set; }

    public override void _Ready()
    {
        _command = GetNode<ICommand>(CommandPath);
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
    }

    private void OnButtonDown()
    {
        _command?.Execute();
    }

    private void OnButtonUp()
    {
        _command?.Undo();
    }
}
