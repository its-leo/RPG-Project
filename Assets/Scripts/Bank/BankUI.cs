using UnityEngine;

public class BankUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject bankUI;

    Bank bank;

    BankSlot[] slots;

	// Use this for initialization
	void Start () {
        bank = Bank.instance;

        //Add UpdateUI to the EventCallback
        bank.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<BankSlot>();
	}


    public void Close()
    {
        bankUI.SetActive(false);
    }


    void UpdateUI ()
    {
 
        for (int i = 0; i < slots.Length; i++)
               {

                   if (i < bank.items.Count)
                   {
                       slots[i].AddItem(bank.items[i]);
                   } else
                   {
                       slots[i].ClearSlot();
                   }
               }
    }

}
