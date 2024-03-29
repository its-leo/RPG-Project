﻿using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        
        if(focus != null) {
            float distance = Vector3.Distance(this.transform.position, focus.transform.position);
            if (distance <= focus.radius) {
                motor.FaceTarget();
                focus.Interact();
                if(!focus.repeatedInteraction) {
                    motor.StopMovement();
                    RemoveFocus();
                }                
            }
        }
        

        //Left Mouse Button
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                motor.MoveToPoint(hit.point);


                RemoveFocus();
            }
        }

        //Right Mouse Button
        if (Input.GetMouseButtonDown(1)) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {

                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null) {
                    SetFocus(interactable);
                }
                //If we did set it as our focus
            }
        }
    }
    void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);

    }

    void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
