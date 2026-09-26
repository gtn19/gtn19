using System;
using System.Collections.Generic;

/// <summary>
/// Generic state machine, ported from the Love2D StateMachine module.
/// Each state implements IState; the machine handles Enter/Exit/Update transitions
/// and keeps a small history stack if you need to "go back".
/// </summary>
public interface IState
{
    void Enter();
    void Update(float deltaTime);
    void Exit();
}

public class StateMachine
{
    private IState _currentState;
    private readonly Stack<IState> _history = new Stack<IState>();

    public IState CurrentState => _currentState;

    public event Action<IState, IState> OnStateChanged; // (from, to)

    public void ChangeState(IState newState, bool pushToHistory = true)
    {
        if (newState == _currentState) return;

        var previous = _currentState;
        _currentState?.Exit();

        if (pushToHistory && previous != null)
            _history.Push(previous);

        _currentState = newState;
        _currentState.Enter();

        OnStateChanged?.Invoke(previous, _currentState);
    }

    public void GoBack()
    {
        if (_history.Count == 0) return;
        var previous = _history.Pop();
        ChangeState(previous, pushToHistory: false);
    }

    public void Update(float deltaTime)
    {
        _currentState?.Update(deltaTime);
    }
}
