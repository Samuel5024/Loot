using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    public Stack<string> lootStack = new Stack<string>(); //create an empty stack with string type elements
    public string labelText = "Collect all 4 items and win your freedom!"; //labelText appears at the bottom of the screen
    public int maxItems = 4;
    private int _itemsCollected = 0;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    private string _state;
    public string State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
        }
    }

    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _state = "Manager Intialized.";
        _state.FancyDebug();
        Debug.Log(_state);

        lootStack.Push("Sword of Doom"); //use Push method to add string elements to stack (size increases each time)
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
    }
    public int Items
    {
        get  //get returns the value stored in _itemsCollected
        {
            return _itemsCollected; 
        }

        set //set assigns _itemsCollected to the new value of Items whenever updated
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems) //if player gathers more or equal to maxItems, they win
            {
                labelText = "You've found all the items!";
                showWinScreen = true; //pause the game when the win screen is displayed
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) +
                    " more to go!";
            }
        }
    }

    private int _playerHP = 10;
    public int HP
    {
        get //get and set complements the private _playerHP backing varaible
        { 
            return _playerHP;  
        }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if(_playerHP <= 0)
            {
                labelText = "You Want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }

            else
            {
                labelText = ("Ouch...That's gotta hurt!");
            }
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
    }

    void OnGUI() //house the UI code
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP); //Rect class takes in x, y, width, and height
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100,
            Screen.height - 50, 300, 50), labelText);

        if (showWinScreen) //check if the win screen should be displayed
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "YOU LOST!"))
            {
                Utilities.RestartLevel();
            }
        }
    }

    public void PrintLootReport() //print stack count whenever the method is called 
    {
        Debug.LogFormat("There are {0} random loot items waiting for you",
            lootStack.Count);
    }
}
