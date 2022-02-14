using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update



    void Start()
    {
        BlocoSpawner.instance.RandomSpawn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
