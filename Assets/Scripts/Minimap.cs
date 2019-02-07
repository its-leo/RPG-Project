using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {
    public Transform player;
    public Transform mainCamera;
    private float previousShadowDistance;


void OnPreRender()
    {
        previousShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }
void OnPostRender()
    {
        QualitySettings.shadowDistance = previousShadowDistance;
    }

void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;


        //Rotate Minimap
        transform.rotation = Quaternion.Euler(90f, mainCamera.eulerAngles.y, 0f);

    }
}
