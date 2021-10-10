using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public GameObject zombie;
    public static string carrying;
    public float spawnInterval = 2;
    public bool spawning = false;
    public static int spawned;
    public int maxSpawned = 20;

    public bool planted = false;
    public float timerLength = 1;

    public UnityEvent carryingEmpty;
    public UnityEvent win;

    public GameObject plantObject;
    public Transform plantSpawn;

    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        spawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning && spawned < maxSpawned)
        {
            StartCoroutine(SpawnWave(spawnInterval));
        }
        if (planted)
        {
            StartCoroutine(StartTimer());
            planted = false;
        }
    }

    private IEnumerator SpawnWave(float time)
    {
        spawning = true;
        foreach (var point in spawnPoints)
        {
            GameObject clone = Instantiate(zombie);
            clone.transform.position = point.transform.position;
            spawned++;
        }
        yield return new WaitForSeconds(time);
        spawning = false;
    }

    public void Carry(string item)
    {
        if (carrying != null)
        {
            carryingEmpty.Invoke();
            Debug.Log("You cannot carry another item if you are already carrying one");
            return;
        }
        if (carrying == null)
        {
            carrying = item;
        }
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timerLength*60);
        Instantiate(plantObject, plantSpawn);
        win.Invoke();
    }

    public void DisplayText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    public IEnumerator ShowText(string message)
    {
        infoText.text = message;
        yield return new WaitForSeconds(1);
        infoText.text = null;
    }
}
