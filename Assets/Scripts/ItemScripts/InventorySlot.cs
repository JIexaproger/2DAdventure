[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;   // null = пусто
    public int amount = 0;

    public bool IsEmpty => itemData == null || amount <= 0;

    public InventorySlot()
    {
        // Пустой слот по умолчанию
    }

    public InventorySlot(ItemData itemData, int amount = 1)
    {
        this.itemData = itemData;
        this.amount = amount;
    }
}