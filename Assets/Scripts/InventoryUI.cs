using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotUIPrefab;
    private GridLayoutGroup grid;

    private List<SlotUI> slots = new List<SlotUI>();

    public int width = 5;
    public int height = 3;

    private void Awake()
    {
        grid = gameObject.GetComponent<GridLayoutGroup>();
    }
    private void Start()
    {
        CreateSlots();
        inventory.OnSlotChanged += OnSlotChanged; // Подписка!
        RefreshAllSlots();
    }

    private void CreateSlots()
    {
        // Очистить старое
        foreach (var slot in slots)
            Destroy(slot.gameObject);
        slots.Clear();

        // Создать новые
        for (int i = 0; i < inventory.items.Length; i++)
        {
            SlotUI slot = Instantiate(slotUIPrefab, grid.transform).GetComponent<SlotUI>();
            slots.Add(slot);
        }
    }

    private void OnSlotChanged(int slotIndex)
    {
        if (slotIndex < slots.Count)
        {
            var slotUI = slots[slotIndex];
            var slot = inventory.Get(slotIndex);

            if (slot.IsEmpty)
                slotUI.Clear();
            else
                slotUI.Set(slot.itemData.Sprite, slot.amount);
        }
    }

    private void RefreshAllSlots()
    {
        for (int i = 0; i < slots.Count; i++)
            OnSlotChanged(i);
    }

    private void OnDestroy()
    {
        if (inventory) inventory.OnSlotChanged -= OnSlotChanged;
    }
}