using Godot;
using Pigdom.Game;
using Pigdom.Recipes;

namespace Pigdom.Objects;

public partial class Diamond : RigidBody2D
{
  private ScorePoint _scorePoint;
  private InteractiveArea2D _interactiveArea2D;

  public override void _Ready()
  {
    _scorePoint = GetNode<ScorePoint>("ScorePoint");
    _interactiveArea2D = GetNode<InteractiveArea2D>("InteractiveArea2D");
    _interactiveArea2D.InteractionAvailable += OnInteractiveArea2DInteractionAvailable;
  }

  private void OnInteractiveArea2DInteractionAvailable()
  {
    _scorePoint.IncreaseScore();
    QueueFree();
  }
}
