using UnityEngine;

public class Food : PickupableItem
{
    [SerializeField] private FoodObject foodObject;

    public int GetSatiety()
    {
        return foodObject.Satiety;
    }
}
