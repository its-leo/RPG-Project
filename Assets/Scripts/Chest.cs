using UnityEngine;
using System.Collections;

public class Chest : Interactable {

    [Range(0.0f, 1.0f)]
    public float factor;

    Quaternion closedAngle;
    Quaternion openedAngle;

    bool closing;
    bool opening;

    float speed = 0.8f;

    int newAngle = 127;

    // Use this for initialization
    void Start () {
        openedAngle = interactionTransform.rotation;
        closedAngle = Quaternion.Euler(interactionTransform.eulerAngles + Vector3.right * newAngle);

        if (interactionTransform.rotation == closedAngle)
        {
            closing = true;
        }
        if (interactionTransform.rotation == openedAngle)
        {
            opening = true;
        }
    }


    public override void Interact()
    {
        closing = !closing;
        opening = !opening;
    }


    // Update is called once per frame
    public override void Update() {
        base.Update();

        if (closing)
        {
            factor += speed * Time.deltaTime;

            if (factor > 1.0f)
            {
                factor = 1.0f;
            }
        }
        if (opening)
        {
            factor -= speed * Time.deltaTime;

            if (factor < 0.0f)
            {
                factor = 0.0f;
            }
        }
        interactionTransform.rotation = Quaternion.Lerp(openedAngle, closedAngle, factor);
	}


}
