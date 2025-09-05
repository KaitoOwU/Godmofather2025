using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _imgSlot;
    [SerializeField] private Image _imgItem;

    public event Action OnWin;
    public bool IsFull => _inventory[0] != null;

    private Book[] _inventory = new Book[1]; 
    private int _nbBooks = 6; //books to collect

    //Scripts
    private PlayerController _pc;

    private GameObject _myPlace;

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

    private void ResetInventory()
    {
        _inventory = new Book[1];
        ViewItem(false);
    }

    // -- PLACING --
    public void PlaceBook()
    {
        // Clear inventory
        _inventory[0] = null;
        ViewItem(false);

        //check the number of books stored
        CheckNbBooksStored();

        //visual book in library
        _myPlace.GetComponent<Place>().FillFreePlace();
        //supprimer le book placement
        //afficher le book dans la biblio

        //reset controls after place book
        _pc.SetControls(true);
    }

    public void SetMyPlace(GameObject p)
    {
        _myPlace = p;
    }


    private void ViewItem(bool visible)
    {
        _imgItem.enabled = visible;
    }

    private void CheckNbBooksStored()
    {
        _nbBooks -= 1;

        if (_nbBooks == 0)
        {
            // -- DEBUG --
            //Debug.Log("Win");

            OnWin?.Invoke();
            //_pc.SetControls(false); // to edit marche pas pr l instant
            // declencher la win activeer le canva
        }
    }

}
