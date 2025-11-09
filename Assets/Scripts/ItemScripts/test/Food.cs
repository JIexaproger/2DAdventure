using UnityEngine;

public class Food : Item
{
    [SerializeField] private FoodObject foodObject;

    public int GetSatiety()
    {
        return foodObject.Satiety;
    }
}
