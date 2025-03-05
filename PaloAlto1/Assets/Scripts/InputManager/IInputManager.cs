using UnityEngine;

public interface IInputManager {
    Vector2 GetDirectionalInput();
    bool StoreDirectionsInput();
    void PrintInputShards();
}