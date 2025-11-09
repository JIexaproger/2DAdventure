using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class ItemObject : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public int MaxAmount;
}
