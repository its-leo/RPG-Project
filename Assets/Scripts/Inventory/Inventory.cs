using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion


    //Create Event for Item Changed
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 24;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (!item.isDefaultItem) {

            if (items.Count >= space) {
                Debug.Log("Inventory full");
                return false;
            }
            else {
                items.Add(item);

                //Invoke Event on Change
                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }

            }
        }
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        //Invoke Event on Change
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

    public void Drop(Item item) {
        Remove(item);
        Transform playerTransform = PlayerManager.instance.player.transform;
        Instantiate(item.itemPrefab, playerTransform.localPosition, playerTransform.localRotation);
    }
}
