using Godot;

public partial class VisibleArea2D : Area2D
{
    [Export(PropertyHint.File, "*InteractionStrategy.cs")]
    public string InteractionStrategyFile { get; set; }

    public InteractionStrategy InteractionStrategy { get; set; }

    public override void _Ready()
    {
        if (!string.IsNullOrEmpty(InteractionStrategyFile))
        {
            var script = GD.Load<CSharpScript>(InteractionStrategyFile);
            InteractionStrategy = script.New().As<InteractionStrategy>();
        }
    }
}
