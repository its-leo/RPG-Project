using UnityEngine;

public class Interactable : MonoBehaviour {

    //How near to interact
    public float radius = 3f;

    public float stoppingDistance = .8f;

    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    public bool repeatedInteraction = false;

    bool inReach = false;


    //Create Event for Item Changed
    public delegate void OnReachChanged();
    public OnReachChanged onReachChangedCallback;



    //This method is meant to be overwritten (therefore virtual)
    public virtual void Interact() {
        Debug.Log("Interacting with " + gameObject.name);
        inReach = true;
    }

    public virtual void Update() {
        if (inReach)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                inReach = false;
            }
        }
    }

    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
    }

    public virtual void OnDefocused() {
        isFocus = false;
        player = null;
    }


    void OnDrawGizmosSelected() {

        if (interactionTransform == null) {
            interactionTransform = transform;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
