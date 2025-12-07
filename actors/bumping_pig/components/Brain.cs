using Godot;
using Pigdom.Actors.BumpingPig.Commands;
using Pigdom.Recipes;

namespace Pigdom.Actors.BumpingPig.Components;

public partial class Brain : Node
{
    [Export(PropertyHint.NodeType, "BumpingPig")]
    public NodePath ActorPath { get; set; }
    public BumpingPig Actor { get; set; }

    [Export(PropertyHint.NodeType, "Node")]
    public NodePath CommandsPath { get; set; }

    private Node _commands;
    private InteractionStrategy _interactionStrategy;

    public AttackCommand AttackCommand { get; set; }
    public JumpCommand JumpCommand { get; set; }
    public PickCommand PickCommand { get; set; }
    public ThrowCommand ThrowCommand { get; set; }
    public TurnCommand TurnLeftCommand { get; set; }
    public TurnCommand TurnRightCommand { get; set; }
    public MoveCommand MoveLeftCommand { get; set; }
    public MoveCommand MoveRightCommand { get; set; }

    private ICommand<BumpingPig>[] _bumpLeftQueue;
    private ICommand<BumpingPig>[] _bumpRightQueue;

    private Area2D _throwVisionArea;
    private Area2D _bombVisionArea;
    private Area2D _visionArea;

    private BumpingEnemy2D _body;
    private bool _isThrowVisionConnected = false;

    public override void _Ready()
    {
        Actor = GetNode<BumpingPig>(ActorPath);

        _commands = GetNode<Node>(CommandsPath);
        AttackCommand = _commands.GetNode<AttackCommand>("AttackCommand");
        JumpCommand = _commands.GetNode<JumpCommand>("JumpCommand");
        PickCommand = _commands.GetNode<PickCommand>("PickCommand");
        ThrowCommand = _commands.GetNode<ThrowCommand>("ThrowCommand");
        TurnLeftCommand = _commands.GetNode<TurnCommand>("TurnLeftCommand");
        TurnRightCommand = _commands.GetNode<TurnCommand>("TurnRightCommand");
        MoveLeftCommand = _commands.GetNode<MoveCommand>("MoveLeftCommand");
        MoveRightCommand = _commands.GetNode<MoveCommand>("MoveRightCommand");

        _bumpLeftQueue = new ICommand<BumpingPig>[] { TurnRightCommand, MoveRightCommand };
        _bumpRightQueue = new ICommand<BumpingPig>[] { TurnLeftCommand, MoveLeftCommand };

        _body = Actor.GetNode<BumpingEnemy2D>("BumpingEnemy2D");
        _visionArea = Actor.GetNode<Area2D>("BumpingEnemy2D/Sprites/VisionArea2D");
        _throwVisionArea = Actor.GetNode<Area2D>("BumpingEnemy2D/Sprites/ThrowVisionArea2D");

        _body.Bumped += OnBumpingEnemy2DBumped;
        _visionArea.AreaEntered += OnVisionArea2DAreaEntered;
        _throwVisionArea.AreaEntered += OnThrowVisionArea2DAreaEntered;
    }

    private void HandleAreaEntered(Area2D area)
    {
        if (area is VisibleArea2D visibleArea)
        {
            _interactionStrategy = visibleArea.InteractionStrategy;
            _interactionStrategy.Context = this;
            _interactionStrategy.InteractedArea = visibleArea;
            _interactionStrategy.Execute();
        }
    }

    public void OnBumpingEnemy2DBumped()
    {
        if (_body.Direction < 0)
        {
            foreach (var command in _bumpLeftQueue)
            {
                command.Execute();
            }
        }
        else
        {
            foreach (var command in _bumpRightQueue)
            {
                command.Execute();
            }
        }
    }

    public void OnVisionArea2DAreaEntered(Area2D area) => HandleAreaEntered(area);

    public void OnThrowVisionArea2DAreaEntered(Area2D area) => HandleAreaEntered(area);
}
