using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item {

    public int healthPoints;

    public override void Use() {
        base.Use();
        PlayerManager.instance.player.GetComponent<PlayerStats>().RecoverHealth(healthPoints);
        RemoveFromInventory();
    }
}
