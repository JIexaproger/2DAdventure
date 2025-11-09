[System.Serializable]
public class InventorySlot
{
    public ItemData item;   // null = пусто
    public int amount = 0;

    public bool IsEmpty => item == null || amount <= 0;
}