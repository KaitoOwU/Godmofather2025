using UnityEngine;

using System.Collections.Generic;
using System;

public class LibraryArea : MonoBehaviour, ITrigger
{
    #region variables
    private GameObject[] _allFreePlaces ;

    private List<Place> _myPlaces = new List<Place>();

    //components
    private Collider2D _coll;

    //scripts
    LibraryManager _libraryManager;

    #endregion

    void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }


    void OnEnable()
    {
        _libraryManager = FindAnyObjectByType<LibraryManager>();

        // Subscribe to the event if the manager is found
        if (_libraryManager != null)
        {
            _libraryManager.OnPlacesGenerated += GetAllFreePlaces;
        }
    }

    void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks
        if (_libraryManager != null)
        {
            _libraryManager.OnPlacesGenerated -= GetAllFreePlaces;
        }
    }

    private void GetAllFreePlaces()
    {
        _allFreePlaces = _libraryManager.FreePlaces;

        //waiting for random pos
        SetMyFreePlaces();
    }

    private void SetMyFreePlaces()
    {
        // check all created places and order them in the different library areas
        foreach (GameObject g in _allFreePlaces)
        {
            //2d
            Vector3 pos = g.transform.position;
            pos.z = _coll.bounds.center.z; 

            if (_coll.bounds.Contains(pos))
            {
                _myPlaces.Add(g.GetComponent<Place>());
            }
        }
    }


    public void OnEnter()
    {
        if (_myPlaces.Count == 0) return;

        //highlight all interactable objects
        foreach (Place p in _myPlaces)
        {
            p.SetVisibility(true);
        }
    }

    public void OnExit()
    {
        if (_myPlaces.Count == 0) return;

        //disable interactability
        foreach (Place p in _myPlaces)
        {
            p.SetVisibility(false);
        }
    }
}
