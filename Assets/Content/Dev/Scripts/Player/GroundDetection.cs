using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] Transform A;
    [SerializeField] Transform B;
    [SerializeField] Transform Player;

    private bool _grounded;
    public bool Grounded => _grounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 AB = B.position - A.position;
        Vector2 normal = Vector2.Perpendicular(AB).normalized;
        Vector3 AC = Player.position - A.position;

        _grounded = Vector3.Dot(AC, normal) < 0;
    }
}
