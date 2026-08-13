using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Axes")]
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";

    [Header("Actions")]
    [SerializeField] private KeyCode confirmKey = KeyCode.Return;
    [SerializeField] private KeyCode confirmAltKey = KeyCode.Space;
    [SerializeField] private KeyCode cancelKey = KeyCode.Escape;
    [SerializeField] private KeyCode pauseKey = KeyCode.P;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode attackKey = KeyCode.LeftControl;

    private InputState _current;
    private InputState _previous;

    public InputState Current => _current;
    public Vector2 Move => _current.Move;
    public bool InputEnabled { get; set; } = true;

    public event Action<Vector2> OnMoveChanged;
    public event Action<GameInputAction> OnActionPressed;
    public event Action<GameInputAction> OnActionReleased;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance != null)
            return;

        var go = new GameObject(nameof(InputManager));
        DontDestroyOnLoad(go);
        go.AddComponent<InputManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        _previous = _current;
        _current = InputEnabled ? ReadInput() : default;

        if (_current.Move != _previous.Move)
            OnMoveChanged?.Invoke(_current.Move);

        RaiseActionEvents();
    }

    public bool GetAction(GameInputAction action) => _current.IsHeld(action);

    public bool GetActionDown(GameInputAction action) =>
        _current.IsHeld(action) && !_previous.IsHeld(action);

    public bool GetActionUp(GameInputAction action) =>
        !_current.IsHeld(action) && _previous.IsHeld(action);

    private InputState ReadInput()
    {
        var move = new Vector2(
            Input.GetAxisRaw(horizontalAxis),
            Input.GetAxisRaw(verticalAxis));

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        return new InputState(
            move,
            IsAnyKeyHeld(confirmKey, confirmAltKey) || Input.GetButton("Submit"),
            IsAnyKeyHeld(cancelKey) || Input.GetButton("Cancel"),
            IsAnyKeyHeld(pauseKey),
            IsAnyKeyHeld(interactKey),
            IsAnyKeyHeld(attackKey) || Input.GetButton("Fire1"));
    }

    private void RaiseActionEvents()
    {
        foreach (GameInputAction action in Enum.GetValues(typeof(GameInputAction)))
        {
            if (GetActionDown(action))
                OnActionPressed?.Invoke(action);
            else if (GetActionUp(action))
                OnActionReleased?.Invoke(action);
        }
    }

    private static bool IsAnyKeyHeld(params KeyCode[] keys)
    {
        for (var i = 0; i < keys.Length; i++)
        {
            if (Input.GetKey(keys[i]))
                return true;
        }

        return false;
    }
}
