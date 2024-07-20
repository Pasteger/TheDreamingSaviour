using System;
using UnityEngine;

[Serializable]
public class DropItem
{
    public GameObject item;
    [Range(0, 1)] public float chance;
}
