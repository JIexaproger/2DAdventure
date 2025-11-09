using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemObject itemObject;

    public int Amount;


    public string GetName()
    {
        return itemObject.Name;
    }
    public string GetDescription()
    {
        return itemObject.Description;
    }
}
