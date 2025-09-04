using Unity.VisualScripting;
using UnityEngine;

public class Book : MonoBehaviour, IInteractable

{
    InventoryManager _inventoryMgr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inventoryMgr = FindAnyObjectByType<InventoryManager>();
    }

    public void Interact()
    {
        // -- DEBUG --
        //Debug.Log("Interact with a book");

        _inventoryMgr.GetItem(this);
    }

}
