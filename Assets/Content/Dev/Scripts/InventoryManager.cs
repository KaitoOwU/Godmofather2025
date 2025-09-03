using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Image _imgSlot;
    [SerializeField] private Image _imgItem;

    private Book[] _inventory = new Book[1];

    private bool IsFull => _inventory[0] != null;

    private void Awake()
    {
        ResetInventory();
    }

    public void GetItem(Book item)
    {
        if (IsFull) return;

        // add item
        _inventory[0]= item;
        ViewItem(true);
        item.gameObject.SetActive(false);
    }

    public void UseItem(Book item)
    {
        Debug.Log("j utilise cet item " + item.name);

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
