using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int maxAmount;

    public string Name => name;
    public string Description => description;
    public Sprite Sprite => sprite;
    public int MaxAmount => maxAmount;

    public static implicit operator ItemData(bool v)
    {
        throw new NotImplementedException();
    }
}