using System;
using UnityEngine;

public class Malediction : MonoBehaviour
{
    [field:SerializeField] public ParticleSystem Particles { get; private set; }
    [field:SerializeField] public Transform GhostObj { get; private set; }
}
