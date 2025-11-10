using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroCollect : MonoBehaviour
{
    private InputSystem inputSystem;
    private Inventory inventory;
    private List<PickupableItem> pickableItems;
    private int selectedItemIndex;
    public TMP_Text pickUpText;

    // === Инициализация и настройка ===

    private void Awake()
    {
        inputSystem = new InputSystem();
        inventory = gameObject.GetComponent<Inventory>();
        pickableItems = new List<PickupableItem>();
        selectedItemIndex = 0;
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Interact.performed += Interact;
        inputSystem.Player.Previous.performed += Previous;
        inputSystem.Player.Next.performed += Next;
    }
    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Player.Interact.performed -= Interact;
        inputSystem.Player.Previous.performed -= Previous;
        inputSystem.Player.Next.performed -= Next;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.gameObject.GetComponent<PickupableItem>();
        if (item != null)
        {
            pickableItems.Add(item);
            CorrectSelectedIndex(); // Автоматическая коррекция индекса
            UpdateText();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        var item = other.gameObject.GetComponent<PickupableItem>();
        if (item == null || item.gameObject == null) return; // Предмет уже уничтожен
        
        int index = pickableItems.IndexOf(item);
        if (index >= 0)
        {
            pickableItems.RemoveAt(index);
            if (index <= selectedItemIndex)
            {
                selectedItemIndex = Mathf.Max(0, selectedItemIndex - 1);
            }
            UpdateText();
        }
    }


    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (pickableItems.Count == 0) return;

        PickupableItem interactableItem = pickableItems[selectedItemIndex];

        Debug.Log($"Подобрано: {interactableItem.itemData.Name}");
        // int emptySlot = inventory.FindEmptySlot();

        inventory.Add(interactableItem.itemData, interactableItem.Amount);
        Destroy(interactableItem.gameObject);

        CorrectSelectedIndex();
        UpdateText();
    }

    private void Previous(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveSelection(-1);
    }

    private void Next(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveSelection(1);
    }

    // === ВЫНЕСЕННАЯ ЛОГИКА ВЫБОРА ===
    
    private void MoveSelection(int direction)
    {
        if (pickableItems.Count == 0) return;

        int newIndex = selectedItemIndex + direction;
        
        if (newIndex < 0)
            selectedItemIndex = pickableItems.Count - 1; // Циклический переход к концу
        else if (newIndex >= pickableItems.Count)
            selectedItemIndex = 0; // Циклический переход к началу
        else
            selectedItemIndex = newIndex;

        UpdateText();
    }

    private void CorrectSelectedIndex()
    {
        if (pickableItems.Count == 0)
        {
            selectedItemIndex = 0;
            return;
        }

        // Ограничиваем индекс диапазоном [0, Count-1]
        selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, pickableItems.Count - 1);
    }

    private void UpdateText()
    {
        if (pickableItems.Count > 0)
        {
            pickUpText.text = $"Подобрать {selectedItemIndex + 1}/{pickableItems.Count}: {pickableItems[selectedItemIndex].itemData.Name}";
        }
        else
        {
            pickUpText.text = string.Empty;
        }
    }
}