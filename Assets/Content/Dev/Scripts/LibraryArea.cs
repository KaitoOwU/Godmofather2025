using UnityEngine;

using System.Collections.Generic;

public class LibraryArea : MonoBehaviour, ITrigger
{
    #region
    // to randomize later
    [SerializeField] private List<Transform> _allFreePlaces = new List<Transform>();

    private List<FreePlace> _myPlaces = new List<FreePlace>();

    //components
    private Collider2D _coll;

    #endregion

    void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }

    void Start()
    {
        //waiting for random pos
        GetAllMyFreePlaces();
    }

    private void GetAllMyFreePlaces()
    {
        // check all created places and order them in the different library areas
        foreach (Transform t in _allFreePlaces)
        {
            //2d
            Vector3 pos = t.position;
            pos.z = _coll.bounds.center.z; 

            if (_coll.bounds.Contains(pos))
            {
                _myPlaces.Add(t.GetComponent<FreePlace>());
            }
        }
    }


    public void OnEnter()
    {
        if (_myPlaces.Count == 0) return;

        //highlight all interactable objects
        foreach (FreePlace p in _myPlaces)
        {
            p.SetVisibility(true);
        }
    }

    public void OnExit()
    {
        if (_myPlaces.Count == 0) return;

        //disable interactability
        foreach (FreePlace p in _myPlaces)
        {
            p.SetVisibility(false);
        }
    }
}
