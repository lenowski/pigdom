using Godot;
using Pigdom.Interface.PopLabel;
using Pigdom.Recipes.InteractiveArea2D;

namespace Pigdom.Objects.Diamond;

public partial class Diamond : RigidBody2D
{
    [Export]
    private PackedScene PopLabelScene { get; set; } =
        GD.Load<PackedScene>("res://interface/pop_label/PopLabel.tscn");

    [Export]
    private int Score { get; set; } = 175;

    private InteractiveArea2D _interactiveArea2D;

    public override void _Ready()
    {
        _interactiveArea2D = GetNode<InteractiveArea2D>("InteractiveArea2D");
        _interactiveArea2D.InteractionAvailable += OnInteractiveArea2DInteractionAvailable;
    }

    private void OnInteractiveArea2DInteractionAvailable()
    {
        var popLabel = PopLabelScene.Instantiate<PopLabel>();
        FindParent("Level").FindChild("PopLabels").AddChild(popLabel);

        popLabel.Pop(Score.ToString(), GlobalPosition);

        QueueFree();
    }
}
