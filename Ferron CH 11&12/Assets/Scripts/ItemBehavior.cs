using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    //new variable of the GameBehavior type
    public GameBehavior gameManager;

    void Start()
    {
        //Start() initializes gameManager by looking it up in the scene with Find() & ading a call to GetComponent
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    //unity automatically calls OnCollision when another object (isTrigger turned off) runs into the item prefab
    void OnCollisionEnter(Collision collision)
    {
        //the property, gameObject, holds reference to the colliding gameObject's Collider
        if (collision.gameObject.name == "Player")
        {
            //destroys the entire Item prefab when collided with by the Player
            Destroy(this.transform.parent.gameObject);

            //prints that we collected the item
            Debug.Log("Item Collected!");

            //increment items property after Item prefab is destroyed
            gameManager.Items += 1;
        }
    }
}
