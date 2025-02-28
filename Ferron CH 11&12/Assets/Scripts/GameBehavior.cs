using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
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
    }

    //labelText appears at the bottom of the screen
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    private int _itemsCollected = 0;
    //tracks when the win screen should appear
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public int Items
    {
        //get returns the value stored in _itemsCollected
        get 
        {
            return _itemsCollected; 
        }
        
        //set assigns _itemsCollected to the new value of Items whenever updated
        set
        {
            _itemsCollected = value;
            //if player gathers more or equal to maxItems, they win
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                //pause the game when the win screen is displayed
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
        //get and set complements the private _playerHP backing varaible
        get 
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

    //house the UI code
    void OnGUI()
    {
        //Rect class takes in x, y, width, and height 
        GUI.Box(new Rect (20, 20, 150, 25), "Player Health: " + _playerHP);

        //display the item count
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);

        //display labelText
        GUI.Label(new Rect(Screen.width / 2 - 100,
            Screen.height - 50, 300, 50), labelText);
        
        //check if the win screen should be displayed
        if(showWinScreen)
        {
            if(GUI.Button(new Rect(Screen.width / 2 - 100, 
                Screen.height /2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }

        if(showLossScreen)
        {
            if(GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "YOU LOST!"))
            {
                Utilities.RestartLevel();
            }
        }
    }
}
