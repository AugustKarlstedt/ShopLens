using UnityEngine;

public abstract class HoldableGameObject : MonoBehaviour
{
    public abstract void OnHoldStarted();
    public abstract void OnHoldCanceled();
    public abstract void OnHoldCompleted();
}
