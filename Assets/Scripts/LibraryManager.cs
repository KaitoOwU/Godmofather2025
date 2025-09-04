using System;

using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class LibraryManager : MonoBehaviour
{
    #region Variabales
    public GameObject[] FreePlaces { get; private set; }
    public event Action OnPlacesGenerated;

    private GameObject[] _allPlaces;

    private int _nbBooks = 6;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //get all places
        _allPlaces = GameObject.FindGameObjectsWithTag("Place");

        // set random places
        GenerateRandomFreePlaces();
    }

    private void Start()
    {
        OnPlacesGenerated?.Invoke();
    }

    private void GenerateRandomFreePlaces()
    {
        List<GameObject> tempFreePlaces = new List<GameObject>();

        while (tempFreePlaces.Count < _nbBooks)
        {
            // get rd object
            int rdIndex = UnityEngine.Random.Range(0, _allPlaces.Length);
            GameObject rdbject = _allPlaces[rdIndex];

            // Check if already picked
            if (!tempFreePlaces.Contains(rdbject))
            {
                tempFreePlaces.Add(rdbject);

                //udpate object state
                rdbject.GetComponent<Place>().BecameFreePlace();
            }
        }

        //update free places array
        FreePlaces = tempFreePlaces.ToArray();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
