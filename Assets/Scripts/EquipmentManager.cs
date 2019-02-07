using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : MonoBehaviour {

    #region Singleton


    public static EquipmentManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<EquipmentManager>();
            }
            return _instance;
        }
    }
    static EquipmentManager _instance;

    void Awake() {
        _instance = this;
    }

    #endregion

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    // Callback for when an item is equipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public event OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start() {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }



    // Equip a new item
    public void Equip(Equipment newItem) {
        // Find out what slot the item fits in
        // and put it there.
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex); ;
        // If there was already an item in the slot
        // make sure to put it back in the inventory
        if (currentEquipment[slotIndex] != null) {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);


        currentEquipment[slotIndex] = newItem;


        //Instantiate Mesh into GameWorld
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;


    }

    public bool isEquiped(string name, EquipmentSlot slot) {
        bool equiped = false;

        if (currentEquipment[(int)slot] != null) {
            if (currentEquipment[(int)slot].name.Equals(name)) {
                equiped = true;
            }
        }
        return equiped;
    }




    public Equipment Unequip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            if (currentMeshes[slotIndex] != null) {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;


            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    void UnequipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    //Resize body regions to fit equipment without blending over
    void SetEquipmentBlendShapes(Equipment item, int weight) {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems() {
        foreach (Equipment item in defaultItems) {
            Equip(item);
        }
    }
}
