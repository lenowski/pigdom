using Godot;
using Pigdom.Actors.BumpingPig.Commands;
using Pigdom.Objects.Bomb;
using Pigdom.Recipes;

namespace Pigdom.Actors.BumpingPig.Components;

public partial class Brain : Node
{
    [Export(PropertyHint.NodeType, "BumpingEnemy2D")]
    public NodePath ActorPath { get; set; }

    [Export(PropertyHint.NodeType, "Node")]
    public NodePath CommandsPath { get; set; }

    private BumpingEnemy2D _actor;
    private Node _commands;

    private AttackCommand _attackCommand;
    private JumpCommand _jumpCommand;
    private PickBombCommand _pickBombCommand;
    private ThrowBombCommand _throwBombCommand;
    private TurnCommand _turnLeftCommand;
    private TurnCommand _turnRightCommand;
    private MoveCommand _moveLeftCommand;
    private MoveCommand _moveRightCommand;

    private ICommand<BumpingPig>[] _bumpLeftQueue;
    private ICommand<BumpingPig>[] _bumpRightQueue;

    private Area2D _throwVisionArea;
    private Area2D _bombVisionArea;
    private Area2D _visionArea;

    private bool _isThrowVisionConnected = false;

    public override void _Ready()
    {
        _actor = GetNode<BumpingEnemy2D>(ActorPath);
        _commands = GetNode<Node>(CommandsPath);

        _attackCommand = _commands.GetNode<AttackCommand>("AttackCommand");
        _jumpCommand = _commands.GetNode<JumpCommand>("JumpCommand");
        _pickBombCommand = _commands.GetNode<PickBombCommand>("PickBombCommand");
        _throwBombCommand = _commands.GetNode<ThrowBombCommand>("ThrowBombCommand");
        _turnLeftCommand = _commands.GetNode<TurnCommand>("TurnLeftCommand");
        _turnRightCommand = _commands.GetNode<TurnCommand>("TurnRightCommand");
        _moveLeftCommand = _commands.GetNode<MoveCommand>("MoveLeftCommand");
        _moveRightCommand = _commands.GetNode<MoveCommand>("MoveRightCommand");

        _bumpLeftQueue = new ICommand<BumpingPig>[] { _turnRightCommand, _moveRightCommand };
        _bumpRightQueue = new ICommand<BumpingPig>[] { _turnLeftCommand, _moveLeftCommand };

        _throwVisionArea = _actor.GetNode<Area2D>("Sprites/ThrowVisionArea2D");
        _bombVisionArea = _actor.GetNode<Area2D>("Sprites/BombVisionArea2D");
        _visionArea = _actor.GetNode<Area2D>("Sprites/VisionArea2D");

        _actor.Bumped += OnBumpingEnemy2DBumped;
        _visionArea.AreaEntered += OnVisionArea2DAreaEntered;
        _bombVisionArea.AreaEntered += OnBombVisionArea2DAreaEntered;
    }

    public void OnBumpingEnemy2DBumped()
    {
        if (_actor.Direction > 0)
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

    public void OnVisionArea2DAreaEntered(Area2D area)
    {
        _attackCommand.Execute();
    }

    public void OnBombVisionArea2DAreaEntered(Area2D area)
    {
        var bomb = area.Owner as Bomb;

        _pickBombCommand.Bomb = bomb;
        _pickBombCommand.Execute();

        // IsConnected() won't work - each Callable.From() creates a new instance, so we track state manually
        if (!_isThrowVisionConnected)
        {
            _throwVisionArea.AreaEntered += OnThrowVisionArea2DAreaEntered;
            _isThrowVisionConnected = true;
        }
    }

    public void OnThrowVisionArea2DAreaEntered(Area2D area)
    {
        _throwVisionArea.AreaEntered -= OnThrowVisionArea2DAreaEntered;
        _isThrowVisionConnected = false;

        _throwBombCommand.Execute();
    }
}
