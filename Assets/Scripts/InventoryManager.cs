using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _imgSlot;
    [SerializeField] private Image _imgItem;

    private Book[] _inventory = new Book[1];

    private bool IsFull => _inventory[0] != null;
    private PlayerController _pc;

    private void Awake()
    {
        ResetInventory();
    }

    private void Start()
    {
        _pc = FindAnyObjectByType<PlayerController>();
    }

    public void GetItem(Book item)
    {
        if (IsFull) return;

        // add item
        _inventory[0]= item;
        ViewItem(true);
        item.gameObject.SetActive(false);

        //init coll iinteractable
        _pc.ResetInteractablity();
    }

    public void PlaceBook()
    {
        // Clear
        _inventory[0] = null;
        ViewItem(false);
    }


    private void ResetInventory()
    {
        _inventory = new Book[1];
        ViewItem(false);
    }

    private void ViewItem(bool visible)
    {
        _imgItem.enabled = visible;
    }

}
