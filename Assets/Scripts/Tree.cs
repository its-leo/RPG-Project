using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable {

    PlayerAnimator player;
    Inventory inventory;
    public Item spawnedLog;

    void Start() {
        player = PlayerManager.instance.player.GetComponent<PlayerAnimator>();
        inventory = Inventory.instance;
    }

    // Chop
    public override void Interact() {
        if (EquipmentManager.instance.isEquiped("Axe", EquipmentSlot.Weapon)) {
            repeatedInteraction = true;
            player.TriggerAnimation("chop", true);
            base.Interact();

            int random = Random.Range(0, 1000);

            if (random < 5) {
                inventory.Add(spawnedLog);
                Debug.Log("We've got a " + spawnedLog.name);
            }

        }
        else {
            repeatedInteraction = false;
            Debug.Log("We need an axe to chop a tree");
        }
    }
}