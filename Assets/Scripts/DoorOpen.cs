using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private bool opened = false;
    private bool closing = false;
    private float temp = 0;

    public void OpenDoor()
    {
        opened = true;
        closing = false;
    }

    private void Update()
    {
        if (opened && temp < 2)
        {
            transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0), Space.Self);
            temp += Time.deltaTime;
        }
        if (closing && temp > 0)
        {
            transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0), Space.Self);
            temp -= Time.deltaTime;
        }
    }
    public void DoorClosing()
    {
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(6);
        opened = false;
        closing = true;
    }
}
