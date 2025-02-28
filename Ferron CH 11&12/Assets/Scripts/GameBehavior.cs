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
    public delegate void DebugDelegate(string newText); //public delegate type named DebugDelegate to hold a method that takes in a string
    public DebugDelegate debug = Print; //new DebugDelegate instance named debug & assigns it a method w/ a mathcing signature Print();
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
        InventoryList<string> inventoryList = new //create new instance of InventoryList that holds string values
            InventoryList<string>();

        inventoryList.SetItem("Potion"); //assign a string value to the item property on inventoryList usign setItem()
        Debug.Log(inventoryList.item);
    }
    public void Initialize()
    {
        _state = "Manager Intialized.";
        _state.FancyDebug();
        debug(_state); //call to debug delegate
        LogWithDelegate(debug); //calls LogwithDelegate & passes in our debug variable as its type

        lootStack.Push("Sword of Doom"); //use Push method to add string elements to stack (size increases each time)
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");

        GameObject player = GameObject.Find("Player"); //find Player object in scene & stores its GameObject in a local variable
        PlayerBehavior playerBehavior = //retrieve a reference to the PlayerBehavior class attached 
            player.GetComponent<PlayerBehavior>(); //to the P+layer & stores it in a local varaible
        playerBehavior.playerJump += HandlePlayerJump; //Subscribes to the playerJump event with the method HandlePlayerJump
    }

    public void HandlePlayerJump() //declares HandlePlayerJump() w/ a signatrue that matches the event type &
    {                              //logs a success messge using the debug delegate whenever the event is received
        debug("Player has jumped...");
    }

    public static void Print(string newText) //decalres Print() as a static method that takes in a string & logs it to the console
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del) //new method that takes in a parameter of the DebugDelegate type
    {
        del("Delegating the debug task..."); //calls delegate parameter's function & passes in a string literal to be printed out
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
                try //moves call to RestartLevel() inside w/ a debug command to print if the restart is completed w/o any exceptions
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully");
                }

                catch (System.ArgumentException e) //defines System.ArgumentException as the exception type it will handle and e as the local variable name
                {
                    Utilities.RestartLevel(0); //Restarts game if exception is thrown: debug delegate prints out a custom message
                    debug("Reverting to scene 0: " + e.ToString()); //w/ the exception info which can be accessed from e and converted into a string
                }

                finally //signify the end of the excption handling code
                {
                    debug("Restart handled...");
                }
            }
        }
    }

    public void PrintLootReport() //print stack count whenever the method is called 
    {
        var currentItem = lootStack.Pop(); //removes the next item on the stack & stores it 
        var nextItem = lootStack.Peek(); //stores next item on the stack without removing it
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you",
            lootStack.Count);
    }
}
