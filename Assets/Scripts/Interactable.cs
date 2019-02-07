using UnityEngine;

public class Interactable : MonoBehaviour {

    //How near to interact
    public float radius = 3f;

    public float stoppingDistance = .8f;

    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;
    public bool repeatedInteraction = false;




    //This method is meant to be overwritten (therefore virtual)
    public virtual void Interact() {
        //Debug.Log("Interacting with " + gameObject.name);
    }

    public virtual void Update() {
        hasInteracted = true;
        /*
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius) {
                Interact();
                hasInteracted = true; 
            }
        }*/
    }

    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public virtual void OnDefocused() {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


    void OnDrawGizmosSelected() {

        if (interactionTransform == null) {
            interactionTransform = transform;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
