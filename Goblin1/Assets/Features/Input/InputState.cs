using UnityEngine;

public readonly struct InputState
{
    public Vector2 Move { get; }
    public bool Confirm { get; }
    public bool Cancel { get; }
    public bool Pause { get; }
    public bool Interact { get; }
    public bool Attack { get; }

    public InputState(
        Vector2 move,
        bool confirm,
        bool cancel,
        bool pause,
        bool interact,
        bool attack)
    {
        Move = move;
        Confirm = confirm;
        Cancel = cancel;
        Pause = pause;
        Interact = interact;
        Attack = attack;
    }

    public bool IsHeld(GameInputAction action)
    {
        return action switch
        {
            GameInputAction.Confirm => Confirm,
            GameInputAction.Cancel => Cancel,
            GameInputAction.Pause => Pause,
            GameInputAction.Interact => Interact,
            GameInputAction.Attack => Attack,
            _ => false
        };
    }
}
