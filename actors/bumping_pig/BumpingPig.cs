using System;
using System.Linq;
using Godot;
using Pigdom.Actors.Interfaces;
using Pigdom.Actors.States;
using Pigdom.Extensions;
using Pigdom.Game;
using Pigdom.Recipes;

namespace Pigdom.Actors;

public partial class BumpingPig : Node2D
{
    [Export]
    public int InitialDirection { get; set; } = -1;

    [Export]
    public int MaxHealth { get; set; } = 3;

    public IBumpingPigState CurrentState => _stateManager.CurrentState;
    public int CurrentHealth => _health;
    public int CurrentDirection => _body.Direction;

    public event Action<int> DirectionChanged;
    public event Action<Area2D> PlayerDetected;
    public event Action<StringName> AnimationFinished;

    private BumpingEnemy2D _body;
    private Area2D _visionArea;
    private AnimationTree _animationTree;
    private Node2D _sprites;
    private ScorePoint _score;
    private Node _loots;
    private int _health;
    private BumpingPigStateManager _stateManager;

    public override void _Ready()
    {
        var hurtArea = GetNode<HurtArea2D>("BumpingEnemy2D/HurtArea2D");

        _body = GetNode<BumpingEnemy2D>("BumpingEnemy2D");
        _visionArea = GetNode<Area2D>("BumpingEnemy2D/VisionArea2D");
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _sprites = GetNode<Node2D>("BumpingEnemy2D/Sprites");
        _score = GetNode<ScorePoint>("BumpingEnemy2D/ScorePoint");
        _loots = GetNode<Node>("BumpingEnemy2D/Loots");

        _health = MaxHealth;
        _body.Direction = InitialDirection;

        _stateManager = new BumpingPigStateManager();
        InitializeStates();
        _stateManager.TransitionTo<BumpingPigIdleState>();

        hurtArea.Hurt += OnHurtArea2DHurt;
        _body.DirectionChanged += (direction) => DirectionChanged?.Invoke(direction);
        _visionArea.AreaEntered += (area) => PlayerDetected?.Invoke(area);
        _animationTree.AnimationFinished += (animationName) =>
            AnimationFinished?.Invoke(animationName);
    }

    public void EnableAnimation(string condition)
    {
        _animationTree.EnableCondition(condition);
    }

    public void DisableAnimation(string condition)
    {
        _animationTree.DisableCondition(condition);
    }

    public void EnablePhysics()
    {
        _body.SetPhysicsProcess(true);
    }

    public void DisablePhysics()
    {
        _body.SetPhysicsProcess(false);
    }

    public void FlipSprite(int direction)
    {
        var baseScale = _sprites.Scale;

        _sprites.Scale = direction switch
        {
            > 0 => baseScale with { X = -1 },
            < 0 => baseScale with { X = 1 },
            _ => baseScale,
        };
    }

    public void DropLoot()
    {
        foreach (var loot in _loots.GetChildren().OfType<Loot>())
        {
            loot.Drop();
        }
    }

    public void IncreaseScore()
    {
        _score.IncreaseScore();
    }

    public void TransitionTo<TState>()
        where TState : class, IBumpingPigState
    {
        _stateManager.TransitionTo<TState>();
    }

    public void TransitionTo(IBumpingPigState state)
    {
        _stateManager.TransitionTo(state);
    }

    private void InitializeStates()
    {
        var states = GetNode<Node>("States");

        foreach (var state in states.GetChildren().OfType<IBumpingPigState>())
        {
            state.Context = this;
            _stateManager.RegisterState(state);
        }
    }

    private void OnHurtArea2DHurt(int damage)
    {
        _health -= damage;
        _stateManager.HandleEvent(state => state.GetHurt());
    }
}
