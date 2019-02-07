using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    PlayerManager playerManager;

    CharacterStats myStats;
    CharacterCombat playerCombat;

    bool isBeingAttacked = false;



    void Start() {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        repeatedInteraction = true;
    }

    public override void Interact() {
        base.Interact();

        playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null && repeatedInteraction) {
            isBeingAttacked = true;
        }

    }

    public override void Update() {
        base.Update();
        if (isBeingAttacked) {
            playerCombat.Attack(myStats);
        }
    }

    public override void OnDefocused() {
        base.OnDefocused();
        isBeingAttacked = false;
    }
}