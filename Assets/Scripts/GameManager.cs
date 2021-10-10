using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public GameObject zombie;


    // Start is called before the first frame update
    void Start()
    {
        GameObject clone = Instantiate(zombie);
        clone.transform.position = spawnPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
