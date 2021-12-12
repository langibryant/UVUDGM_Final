using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject soldierPrefab;
    public List<GameObject> soldiers;

    public bool spawnSoldier = false;

    public float soldierCost = 50.0f;

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    void OnMouseDown() {

    }

    void FixedUpdate() {
    }

    void InputHandler() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            UI.instance.TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            spawnSoldier = !spawnSoldier;
            string message = spawnSoldier ? "Click to Create Soldier" : "Press F to toggle Spawn Soldier -50g";
            if(UI.instance.IsPaused()){
                message = "";
            }
            UI.instance.SetSpawnMessage(message);
        }
        else {
            string message = "";
            if (UI.instance.IsPaused()){
                message = "";
            }
            else {
                message = spawnSoldier ? "Click to Create Soldier" : "Press F to toggle Spawn Soldier -50g";
            }
            UI.instance.SetSpawnMessage(message);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (spawnSoldier && !UI.instance.IsPaused())
            {
                if(GameManager.instance.playerMoney >= soldierCost) {
                    var screenPoint = Input.mousePosition;
                    screenPoint.z = 10.0f; //distance of the plane from the camera
                    GameObject newSoldier = Instantiate(soldierPrefab, Camera.main.ScreenToWorldPoint(screenPoint), Quaternion.identity);
                    soldiers.Add(newSoldier);
                    GameManager.instance.UpdateGold(soldierCost * -1.0f);    
                }

            }
        }
    }
}
