using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;

public class BloomTable : MonoBehaviour
{
    public UnityEvent carryingEmpty;
    private bool hasSeed = false;
    private bool hasCan = false;
    private Material original;
    public Material flashMat;
    public GameManager gameManager;
    private bool triggered = false;
    //public Color flashColor;

    private void Start()
    {
        original = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void PlaceItem()
    {
        if (GameManager.carrying == null)
        {
            carryingEmpty.Invoke();
            //Debug.Log("You must be carrying an item to place on here");
        }
        else if(GameManager.carrying == "Seed")
        {
            hasSeed = true;
            GameManager.carrying = null;
            gameManager.DisplayText("Seed Planted!");
        } else if(GameManager.carrying == "WateringCan")
        {
            hasCan = true;
            GameManager.carrying = null;
            gameManager.DisplayText("Seed Watered!");
        }
    }

    private void Update()
    {
        if (hasSeed && hasCan && !triggered)
        {
            triggered = true;
            gameManager.planted = true;
            gameManager.DisplayText("Seed Planted and Watered!");
            gameManager.DisplayText("Wait for bloom");
            gameObject.GetComponent<MeshRenderer>().material = original;
        }
    }

    public void Flash()
    {
        gameObject.GetComponent<MeshRenderer>().material = flashMat;
    }
}
