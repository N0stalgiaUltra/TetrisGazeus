using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlocoSpawner : MonoBehaviour
{
    [SerializeField] private List<Bloco> avaliableBlocks = new List<Bloco>(7); // lista de blocos disponiveis
    [SerializeField] private List<Bloco> spawnBlocks; //fila de blocos a serem instanciados
    private int index;
    [SerializeField] public Bloco atual;
    [SerializeField] public Bloco proximo;
    void Start()
    {
        if (spawnBlocks == null)
            spawnBlocks = new List<Bloco>();

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            RandomSpawn();
    }
    public void RandomSpawn()
    {
        //int index = Random.Range(0, avaliableBlocks.Count - 1);
        //GameObject a = Instantiate(avaliableBlocks[index].gameObject, this.transform);
        //print(index);
        // indice do primeiro
        //Bloco atual = null;
        //Bloco proximo = null;

        //atual = 24, prox = 23, count = 25 -> count = 24
        //prix = 22

        if (spawnBlocks.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                index = Random.Range(0, avaliableBlocks.Count - 1);
                spawnBlocks.Add(avaliableBlocks[index]);
            }
        }

        atual = spawnBlocks[0];
        proximo = spawnBlocks[1];
        GameObject obj = Instantiate(atual.gameObject, this.transform);
        spawnBlocks.Remove(spawnBlocks[0]);
        
        index = Random.Range(0, avaliableBlocks.Count - 1);
        spawnBlocks.Add(avaliableBlocks[index]);


    }
    
}
