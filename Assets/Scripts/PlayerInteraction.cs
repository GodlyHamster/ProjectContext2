using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactionRadius = 2f;

    private IInteractable closestInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closestInteractable == null) return;
            closestInteractable.OnInteract();
        }
    }

    private void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(transform.position, interactionRadius);

        float closestDistance = interactionRadius;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (Vector3.Distance(transform.position, collider.transform.position) <= closestDistance)
                {
                    closestInteractable = interactable;
                }
            }
        }
    }
}
