using UnityEngine;
using UnityEngine.Events;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteractEvent = new UnityEvent();

    public void start()
    {
        Debug.Log("I exist");
    }

    public void OnInteract()
    {
        Debug.Log("I invoke");
        OnInteractEvent.Invoke();
    }
}
