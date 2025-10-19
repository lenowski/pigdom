using System;
using System.Collections.Generic;
using Pigdom.Actors.Interfaces;

namespace Pigdom.Actors.States;

public class BumpingPigStateManager
{
    private readonly Dictionary<Type, IBumpingPigState> _states = new();
    private IBumpingPigState _currentState;

    public IBumpingPigState CurrentState => _currentState;

    public void RegisterState(IBumpingPigState state)
    {
        _states[state.GetType()] = state;
    }

    public void TransitionTo<TState>()
        where TState : class, IBumpingPigState
    {
        if (!_states.TryGetValue(typeof(TState), out var newState))
            throw new InvalidOperationException($"State {typeof(TState).Name} not registered");

        TransitionTo(newState);
    }

    public void TransitionTo(IBumpingPigState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            newState.PreviousState = _currentState;
        }

        _currentState = newState;
        _currentState.Enter();
    }

    public void HandleEvent(Action<IBumpingPigState> evnt)
    {
        evnt?.Invoke(_currentState);
    }
}
