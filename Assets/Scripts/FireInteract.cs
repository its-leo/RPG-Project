using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInteract : Interactable {

    public Transform fire;


    public override void Interact() {
        base.Interact();

        fire.gameObject.SetActive(!fire.gameObject.activeSelf);



    }


}
