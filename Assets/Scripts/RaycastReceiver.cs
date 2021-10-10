using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastReceiver : MonoBehaviour
{
    public bool selected = false;
    public UnityEvent onInteract;

    private void Update()
    {
        Debug.Log(selected);

        if (Input.GetKeyDown(KeyCode.F) && selected)
        {
            Interact();
        }
    }

    private void Interact()
    {
        onInteract.Invoke();
    }
}
