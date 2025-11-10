using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] items = new InventorySlot[15];

    private void Awake()
    {
        items = new InventorySlot[15];
    }


    public void Set(int slot, ItemData itemData, int amount)
    {
        items[slot] = new InventorySlot(itemData, amount);
    }

    public InventorySlot Remove(int slot)
    {
        var result = items[slot];
        items[slot] = null;
        return result;
    }

    public void Swap(int slotA, int slotB)
    {
        InventorySlot inventorySlotA = items[slotA];
        items[slotA] = items[slotB];
        items[slotB] = inventorySlotA;
    }


    public InventorySlot Get(int slot)
    {
        return items[slot];
    }


    public bool Add(ItemData itemData, int amount)
    {
        if (amount <= 0) return true;

        int remaining = amount;

        if (TryAddToExistingSlots(itemData, ref remaining))
            return true;

        return TryAddToEmptySlots(itemData, ref remaining);
    }

    // Добавление в слоты с тем же itemData
    private bool TryAddToExistingSlots(ItemData itemData, ref int remaining)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            if (item.itemData == itemData && item.amount < item.itemData.MaxAmount)
            {
                int space = item.itemData.MaxAmount - item.amount;
                int added = Mathf.Min(space, remaining);
                Set(i, itemData, item.amount + added);
                remaining -= added;

                if (remaining == 0) return true;
            }
        }
        return false;
    }

    // Добавление в пустые слоты
    private bool TryAddToEmptySlots(ItemData itemData, ref int remaining)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            if (item.IsEmpty)
            {
                int added = Mathf.Min(remaining, itemData.MaxAmount);
                Set(i, itemData, added);
                remaining -= added;

                if (remaining == 0) return true;
            }
        }
        return remaining == 0;
    }
}



[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Inventory inventory = (Inventory)target;

        if (GUILayout.Button("Показать слот 1"))
        {
            var item = inventory.Get(0);
            Debug.Log($"Слот 0: {item.itemData.Name} - {item.itemData.Description}, {item.amount}");
        }
        if (GUILayout.Button("Показать слот 2"))
        {
            var item = inventory.Get(1);
            Debug.Log($"Слот 1: {item.itemData.Name} - {item.itemData.Description}, {item.amount}");
        }
        if (GUILayout.Button("Показать слот 3"))
        {
            var item = inventory.Get(2);
            Debug.Log($"Слот 2: {item.itemData.Name} - {item.itemData.Description}, {item.amount}");
        }
    }
}
