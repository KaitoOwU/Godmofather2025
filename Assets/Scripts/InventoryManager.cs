using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _imgSlot;
    [SerializeField] private Image _imgItem;

    private Book[] _inventory = new Book[1];
    public bool IsFull => _inventory[0] != null;

    //nb books to collect
    private int _nbBooks = 4;

    //Scripts
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
        // Clear inventory
        _inventory[0] = null;
        ViewItem(false);

        //check the number of books stored
        CheckNbBooksStored();

        //visual book in library
        //supprimer le book placement
        //afficher le book dans la biblio

        //reset controls after place book
        _pc.SetControls(true);
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

    private void CheckNbBooksStored()
    {
        _nbBooks -= 1;

        if (_nbBooks == 0)
            Debug.Log("Win");
            // declencher la win activeer le canva
    }

}
