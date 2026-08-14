using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        var move = _inputHandler.Move;
        if (move.sqrMagnitude < 0.0001f)
            return;

        transform.Translate(move * (moveSpeed * Time.deltaTime), Space.World);
    }
}
