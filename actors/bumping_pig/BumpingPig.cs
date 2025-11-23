using System.Linq;
using Godot;
using Pigdom.Actors.BumpingPig.Commands;
using Pigdom.Actors.BumpingPig.States;
using Pigdom.Game;
using Pigdom.Objects.Bomb;
using Pigdom.Recipes;
using Pigdom.Systems;

namespace Pigdom.Actors.BumpingPig;

public partial class BumpingPig : Node2D
{
    [Export]
    public int InitialDirection { get; set; } = -1;

    [Export]
    public int MaxHealth { get; set; } = 3;

    public BumpingEnemy2D Body { get; private set; }

    public Area2D BombVisionArea { get; private set; }

    public AnimationTree AnimationTree { get; private set; }

    public Node2D Sprites { get; private set; }

    public Node2DFactory BombFactory { get; private set; }

    public ScorePoint Score { get; private set; }

    public Node Loots { get; private set; }

    public int Health { get; set; }

    public State State
    {
        get => _state;
        set
        {
            _state?.Exit();
            if (value is not null)
            {
                value.PreviousState = _state;
            }
            _state = value;
            _state?.Enter();
        }
    }

    private State _state;

    public override void _Ready()
    {
        var hurtArea = GetNode<HurtArea2D>("BumpingEnemy2D/Sprites/HurtArea2D");

        Body = GetNode<BumpingEnemy2D>("BumpingEnemy2D");
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
        Score = GetNode<ScorePoint>("BumpingEnemy2D/ScorePoint");
        Loots = GetNode<Node>("BumpingEnemy2D/Loots");
        Sprites = GetNode<Node2D>("BumpingEnemy2D/Sprites");
        BombVisionArea = GetNode<Area2D>("BumpingEnemy2D/Sprites/BombVisionArea2D");
        BombFactory = GetNode<Node2DFactory>("BumpingEnemy2D/Sprites/BombFactory");

        Health = MaxHealth;
        Body.Direction = InitialDirection;

        InitializeStates();
        InitializeCommands();

        TransitionTo<IdleState>();

        hurtArea.Hurt += OnHurtArea2DHurt;
        Body.Bumped += OnBumpingEnemy2DBumped;

        Move(InitialDirection);
    }

    private void InitializeStates()
    {
        var states = GetNode<Node>("States");
        foreach (var state in states.GetChildren().OfType<State>())
        {
            state.Context = this;
        }
    }

    private void InitializeCommands()
    {
        var commands = GetNode<Node>("Commands");
        foreach (var command in commands.GetChildren().OfType<ICommand<BumpingPig>>())
        {
            command.Receiver = this;
        }
    }

    public void TransitionTo<TState>()
        where TState : State
    {
        var newState = GetNode<TState>($"States/{typeof(TState).Name}");
        TransitionTo(newState);
    }

    public void TransitionTo(State state)
    {
        State = state;
    }

    public void Move(int direction)
    {
        State.Move(direction);
    }

    public void Stop()
    {
        State.Stop();
    }

    public void ThrowBomb(Vector2 throwForce)
    {
        State.ThrowBomb(throwForce);
    }

    public void Jump()
    {
        State.Jump();
    }

    public void CancelJump()
    {
        State.CancelJump();
    }

    public void PickBomb(Bomb bomb)
    {
        State.PickBomb(bomb);
    }

    public void Attack()
    {
        State.Attack();
    }

    public void Turn(int direction)
    {
        State.Turn(direction);
    }

    private void OnHurtArea2DHurt(int damage)
    {
        State.GetHurt(damage);
    }

    public void OnBumpingEnemy2DBumped()
    {
        State.Turn(Body.Direction);
    }
}
