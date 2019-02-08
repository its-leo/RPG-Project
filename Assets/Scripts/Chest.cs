using UnityEngine;
using System.Collections;

public class Chest : Interactable {

    [Range(0.0f, 1.0f)]
    float factor;

    public GameObject bankUI;

    Quaternion closedAngle;
    Quaternion openedAngle;

    bool closing;
    bool opening;
    bool neutral = true;

    public bool chestOpen;

    float speed = 0.95f;

    int newAngle = 127;

    float TIME = 5.0f;
    float timeOpen;

    // Use this for initialization
    void Start () {

        openedAngle = interactionTransform.rotation;
        closedAngle = Quaternion.Euler(interactionTransform.eulerAngles + Vector3.right * newAngle);
         timeOpen = TIME;

        if (chestOpen)
        {
            interactionTransform.rotation = Quaternion.Lerp(openedAngle, closedAngle, 0.0f);
        } else
        {
            Close();
        }
    }

    void resetTimer()
    {
        timeOpen = TIME;
    }


    public override void Interact()
    {
        if(opening && neutral)
        {
                bankUI.SetActive(true);
        } else
        {
            Open();
        }
    }

    void Open()
    {
        neutral = false;
        opening = true;
        closing = false;
        resetTimer();
    }

    void Close()
    {
        neutral = false;
        opening = false;
        closing = true;
    }

    // Update is called once per frame
    public override void Update() {

        if (opening) {
        timeOpen -= Time.deltaTime;
        if (timeOpen < 0)
        {
            if (!bankUI.activeSelf)
            {
                Close();
            }
        }
        }
        if (neutral == false)
        {

            if (closing)
            {
                factor += speed * Time.deltaTime;

                if (factor > 1.0f)
                {
                    factor = 1.0f;
                }
                interactionTransform.rotation = Quaternion.Lerp(openedAngle, closedAngle, factor);
                
                if (factor == 1.0f)
                {
                    neutral = true;
                }

            }
            if (opening)
            {
                factor -= speed * Time.deltaTime;

                if (factor < 0.0f)
                {
                    factor = 0.0f;
                }
                interactionTransform.rotation = Quaternion.Lerp(openedAngle, closedAngle, factor);

                if (factor == 0.0f)
                {
                    neutral = true;
                    bankUI.SetActive(true);
                }
            }           
        }
	}
}
