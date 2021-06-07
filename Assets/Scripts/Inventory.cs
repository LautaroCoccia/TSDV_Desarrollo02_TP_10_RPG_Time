﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Slot
{
    public int ID;
    public int amount;

    public Slot()
    {
        ID = -1;
        amount = 0;
    }
    public Slot(int ID, int amount)
    {
        this.ID = ID;
        this.amount = amount;
    }
    public int AddAmount(int amount)
    {
        this.amount += amount;
        int maxAmount = GameplayManager.GetInstance().GetItemFromID(ID).maxStack;
        if (this.amount > maxAmount)
        {
            int difference = this.amount - maxAmount;
            this.amount = maxAmount;
            return difference;
        }
        else
        {
            return 0;
        }
    }
    public void FillSlot(int ID, int amount)
    {
        this.ID = ID;
        this.amount = amount;
    }
    public void EmptySlot()
    {
        ID = -1;
        amount = 0;
    }
    public bool IsEmpty() { return ID < 0; }

    int SortSlotsByName(string str1, string str2)
    {
        return str1.CompareTo(str2);
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Slot> CurrentItems;
    [SerializeField] int size = 10;
    Equipment equipmentComponent;

    private void Awake()
    {
        equipmentComponent = GetComponent<Equipment>();
        for (int i = 0; i < size; i++)
        {
            Slot newSlot = new Slot();
            CurrentItems.Add(newSlot);
        }
    }

    void Start()
    {
       
    }
    public void SetNewInventory(List<Slot> newInventory)
    {
        CurrentItems.Clear();
        foreach (Slot slot in newInventory)
        {
            CurrentItems.Add(slot);
        }
    }

    public bool AddNewItem(int ID, int amount, int slotPos)
    {
        if (CurrentItems[slotPos].IsEmpty())
        {
            CurrentItems[slotPos].FillSlot(ID, amount);
            return true;
        }
        else
        {
            if(ID == CurrentItems[slotPos].ID && GameplayManager.GetInstance().GetItemFromID(ID).maxStack >= CurrentItems[slotPos].amount + amount)
            {
                CurrentItems[slotPos].AddAmount(amount);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool AddNewItem(int ID, int amount)
    {
        for (int i = 0; i < size; i++)
        {
            if (CurrentItems[i].IsEmpty())
            {
                CurrentItems[i].FillSlot(ID, amount);
                return true;
            }
        }
        return false;
    }

    public void DeleteItem(int slotPos)
    {
        if (!CurrentItems[slotPos].IsEmpty())
        {
            CurrentItems[slotPos].EmptySlot();
        }
    }

    public void SwapItem(int slotPosFrom, int slotPosTo)
    {
        if (slotPosFrom == slotPosTo) return;
        if (!CurrentItems[slotPosFrom].IsEmpty() && !CurrentItems[slotPosTo].IsEmpty())
        {
            Item fromItem = GameplayManager.GetInstance().GetItemFromID(CurrentItems[slotPosFrom].ID);
            Item toItem = GameplayManager.GetInstance().GetItemFromID(CurrentItems[slotPosTo].ID);
            if (fromItem.GetItemType() == toItem.GetItemType() && toItem.maxStack > 1)
            {
                CurrentItems[slotPosFrom].amount = CurrentItems[slotPosTo].AddAmount(CurrentItems[slotPosFrom].amount);
                if (CurrentItems[slotPosFrom].amount <= 0)
                {
                    CurrentItems[slotPosFrom].EmptySlot();
                }
                return;
            }
        }
        Slot temp = new Slot(CurrentItems[slotPosFrom].ID, CurrentItems[slotPosFrom].amount);
        CurrentItems[slotPosFrom] = CurrentItems[slotPosTo];
        CurrentItems[slotPosTo] = temp;
    }

    public void UseItem(int slotPos)    // Doble click o Click Derecho
    {
        if(GameplayManager.GetInstance().GetItemFromID(CurrentItems[slotPos].ID).GetItemType() == ItemType.Consumible)
        {
            CurrentItems[slotPos].AddAmount(-1);
        }
        else
        {
            CurrentItems[slotPos] = equipmentComponent.SwapEquipment(CurrentItems[slotPos]);
        }
    }

    public void Divide(int slotPos)
    {
        if(CurrentItems[slotPos].amount > 1)
        {
            int dividedAmount = (CurrentItems[slotPos].amount / 2) + 1;
            if (AddNewItem(CurrentItems[slotPos].ID, dividedAmount))
            {
                CurrentItems[slotPos].amount /= 2;
            }
        }
        else
        {
            Debug.Log("No puedo dividir");
        }
    }

    public enum SortType { Type, Name, Level }

    public void Sort(int type)
    {
        switch ((SortType)type)
        {
            case SortType.Type:
                SortByName sortNameByType = new SortByName();
                CurrentItems.Sort(sortNameByType);
                SortByType sortType = new SortByType();
                CurrentItems.Sort(sortType);
                break;
            case SortType.Name:
                SortByName sortName = new SortByName();
                CurrentItems.Sort(sortName);
                break;
            case SortType.Level:
                SortByName sortNameByLevel = new SortByName();
                CurrentItems.Sort(sortNameByLevel);
                SortByLevel sortLevel = new SortByLevel();
                CurrentItems.Sort(sortLevel);
                break;
            default:
                Debug.Log("Wrong Sort int from button, can't Sort.");
                break;
        }
    }

    public int GetSize()
    {
        return size;
    }
    public Slot GetSlot(int index)
    {
        return CurrentItems[index];
    }
    public void SetSlot(int index, Slot slot)
    {
        CurrentItems[index] = slot;
    }
    public int GetID(int index)
    {
        return CurrentItems[index].ID;
    }

    class SortByName : IComparer<Slot>
    {
        public int Compare(Slot x, Slot y)
        {
            if (x.IsEmpty()) return 1;
            else if (y.IsEmpty()) return -1;
            return GameplayManager.GetInstance().GetItemFromID(x.ID).name.CompareTo(GameplayManager.GetInstance().GetItemFromID(y.ID).name);
        }
    }
    class SortByLevel : IComparer<Slot>
    {
        public int Compare(Slot x, Slot y)
        {
            if (x.IsEmpty()) return 1;
            else if (y.IsEmpty()) return -1;
            return GameplayManager.GetInstance().GetItemFromID(x.ID).level.CompareTo(GameplayManager.GetInstance().GetItemFromID(y.ID).level);
        }
    }
    class SortByType : IComparer<Slot>
    {
        public int Compare(Slot x, Slot y)
        {
            if (x.IsEmpty()) return 1;
            else if (y.IsEmpty()) return -1;
            return GameplayManager.GetInstance().GetItemFromID(x.ID).GetItemType().CompareTo(GameplayManager.GetInstance().GetItemFromID(y.ID).GetItemType());
        }
    }

    public List<Slot> GetInventoryList()
    {
        return CurrentItems;
    }

}