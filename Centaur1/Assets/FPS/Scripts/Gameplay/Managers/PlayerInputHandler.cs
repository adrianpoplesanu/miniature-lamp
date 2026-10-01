using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Tooltip("Sensitivity multiplier for moving the camera around")]
    public float LookSensitivity = 1f;

    [Tooltip("Used to flip the vertical input axis")]
    public bool InvertYAxis = false;


    [Tooltip("Used to flip the horizontal input axis")]
    public bool InvertXAxis = false;

    PlayerCharacterController m_PlayerCharacterController;
    private InputAction m_LookAction;

    void Start()
    {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();

        m_LookAction = InputSystem.actions.FindAction("Player/Look");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanProcessInput()
    {
        // TODO: implement this
        return true;
    }

    public float GetLookInputsHorizontal()
    {
        if (!CanProcessInput()) return 0.0f;

        float input = m_LookAction.ReadValue<Vector2>().x;

        if (InvertXAxis) input *= -1;

        input *= LookSensitivity;

#if UNITY_WEBGL
            // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
            input *= WebglLookSensitivityMultiplier;
#endif

        return input;
    }

    public float GetLookInputsVertical()
    {
        if (!CanProcessInput()) return 0.0f;

        float input = m_LookAction.ReadValue<Vector2>().y;

        if (InvertYAxis) input *= -1;

        input *= LookSensitivity;

#if UNITY_WEBGL
            input *= WebglLookSensitivityMultiplier;
#endif

        return input;
    }
}
