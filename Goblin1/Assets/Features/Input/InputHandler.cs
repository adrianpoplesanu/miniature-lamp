using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Vector2UnityEvent : UnityEvent<Vector2> { }

public class InputHandler : MonoBehaviour
{
    [SerializeField] private bool listenWhenDisabled;

    [Header("Unity Events")]
    [SerializeField] private Vector2UnityEvent onMoveChanged;
    [SerializeField] private UnityEvent onConfirmPressed;
    [SerializeField] private UnityEvent onCancelPressed;
    [SerializeField] private UnityEvent onPausePressed;
    [SerializeField] private UnityEvent onInteractPressed;
    [SerializeField] private UnityEvent onAttackPressed;

    public Vector2 Move { get; private set; }

    public event Action<Vector2> MoveChanged;
    public event Action ConfirmPressed;
    public event Action CancelPressed;
    public event Action PausePressed;
    public event Action InteractPressed;
    public event Action AttackPressed;

    private void OnEnable()
    {
        Subscribe();
        SyncFromManager();
    }

    private void OnDisable()
    {
        if (!listenWhenDisabled)
            Unsubscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public bool GetAction(GameInputAction action) =>
        InputManager.Instance != null && InputManager.Instance.GetAction(action);

    public bool GetActionDown(GameInputAction action) =>
        InputManager.Instance != null && InputManager.Instance.GetActionDown(action);

    public bool GetActionUp(GameInputAction action) =>
        InputManager.Instance != null && InputManager.Instance.GetActionUp(action);

    private void Subscribe()
    {
        if (InputManager.Instance == null)
            return;

        InputManager.Instance.OnMoveChanged += HandleMoveChanged;
        InputManager.Instance.OnActionPressed += HandleActionPressed;
    }

    private void Unsubscribe()
    {
        if (InputManager.Instance == null)
            return;

        InputManager.Instance.OnMoveChanged -= HandleMoveChanged;
        InputManager.Instance.OnActionPressed -= HandleActionPressed;
    }

    private void SyncFromManager()
    {
        if (InputManager.Instance == null)
            return;

        Move = InputManager.Instance.Move;
    }

    private void HandleMoveChanged(Vector2 move)
    {
        Move = move;
        MoveChanged?.Invoke(move);
        onMoveChanged?.Invoke(move);
    }

    private void HandleActionPressed(GameInputAction action)
    {
        switch (action)
        {
            case GameInputAction.Confirm:
                ConfirmPressed?.Invoke();
                onConfirmPressed?.Invoke();
                break;
            case GameInputAction.Cancel:
                CancelPressed?.Invoke();
                onCancelPressed?.Invoke();
                break;
            case GameInputAction.Pause:
                PausePressed?.Invoke();
                onPausePressed?.Invoke();
                break;
            case GameInputAction.Interact:
                InteractPressed?.Invoke();
                onInteractPressed?.Invoke();
                break;
            case GameInputAction.Attack:
                AttackPressed?.Invoke();
                onAttackPressed?.Invoke();
                break;
        }
    }
}
