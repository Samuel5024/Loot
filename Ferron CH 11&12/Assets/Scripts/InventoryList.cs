using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList <T> where T: class //declare a new generic class with the type T parameter
                                              //only accept types that are classes
{
    private T _item; //adds a public item property of T type w/ private backing variable of same type 
    public T item
    {
        get
        {
            return _item;
        }
    }
    public InventoryList() //add default constructor wiht debug log when a 
                           //new InventoryList instance is created
    {
        Debug.Log("Generic list initialized...");
    }

    public void SetItem (T newItem) //new method that takes in a T type parameter
    {
        _item = newItem; //set value of _item to the generic paramter passed 
                         //into SetItem() and debugs out a success message
        Debug.Log("New item added.");
    }
}
