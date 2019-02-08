using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    #region Singleton

    public static Bank instance;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Bank found");
            return;
        }
        instance = this;
    }

    #endregion


    //Create Event for Item Changed
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 40;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (!item.isDefaultItem) {

            if (items.Count >= space) {
                Debug.Log("Bank full");
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
}
