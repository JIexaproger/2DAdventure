using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;

    private void Awake()
    {
        items = new Item[5];
    }


    public void Put(int slot, Item item)
    {
        items[slot] = item;
    }
    public Item Remove(int slot)
    {
        var result = items[slot];
        items[slot] = null;
        return result;
    }
    public Item Get(int slot)
    {
        return items[slot];
    }
    public int FindEmptySlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) return i;
        }

        return -1; // не найден пустой слот
    }
}



[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Inventory inventory = (Inventory)target;

        if (GUILayout.Button("Init"))
        {
            inventory.items = new Item[5];
        }

        if (GUILayout.Button("Показать инвентарь"))
        {
            for (int i = 0; i < 5; i++)
            {
                // Debug.Log($"Слот {i}: {inventory.Get(i).GetName()} - {inventory.Get(i).GetDescription()}, {inventory.Get(i).Amount}");
                Debug.Log($"Слот {i}: {inventory.Get(i).ToString()}");
            }
        }
        if (GUILayout.Button("Показать слот"))
        {
            Debug.Log($"Слот 0: {inventory.Get(0).GetName()} - {inventory.Get(0).GetDescription()}, {inventory.Get(0).Amount}");
        }
    }
}
