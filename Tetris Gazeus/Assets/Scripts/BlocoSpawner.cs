using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlocoSpawner : MonoBehaviour
{
    #region Singleton
    public static BlocoSpawner instance;
    #endregion
    [Header("Collections")]
    [SerializeField] private List<Bloco> avaliableBlocks = new List<Bloco>(7); // lista de blocos disponiveis
    [SerializeField] private List<Bloco> spawnBlocks; //fila de blocos a serem instanciados
    [SerializeField] private List<GameObject> nextObjects; //fila de blocos a serem instanciados

    private int index;

    [Header("Debug")]
    [SerializeField] private Bloco atual;
    [SerializeField] private Bloco proximo;
    
    [Header("UI")]
    [SerializeField] private Transform nextBlock;
    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
        
        if (spawnBlocks == null)
            spawnBlocks = new List<Bloco>();

    }

    /// <summary>
    /// Instancia um bloco aleatório e deixa salvo o proximo bloco a ser instanciado
    /// </summary>
    public void RandomSpawn()
    {
        //int index = Random.Range(0, avaliableBlocks.Count - 1);
        //GameObject a = Instantiate(avaliableBlocks[index].gameObject, this.transform);
        //print(index);

        if (spawnBlocks.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                index = Random.Range(0, avaliableBlocks.Count);
                spawnBlocks.Add(avaliableBlocks[index]);
            }
        }

        atual = spawnBlocks[0];
        proximo = spawnBlocks[1];
        
        if(nextObjects.Count!= 0)
        {
            Destroy(nextObjects[0]);
            nextObjects.Clear();
            NextBlockSpawn();
        }
        else
            NextBlockSpawn();
        
        GameObject obj = Instantiate(atual.gameObject, this.transform.position, Quaternion.identity);
        spawnBlocks.Remove(spawnBlocks[0]);
        
        index = Random.Range(0, avaliableBlocks.Count - 1);
        spawnBlocks.Add(avaliableBlocks[index]);
    }

    /// <summary>
    /// Spawna o proximo bloco para mostrar ao jogador qual será o tipo de bloco que virá
    /// </summary>
    public void NextBlockSpawn()
    {
        GameObject a = Instantiate(proximo.gameObject, nextBlock.transform);
        a.transform.localPosition = new Vector3(0, 0, -1);
        a.transform.localScale = new Vector3(10, 10, 10);               
        a.GetComponent<Bloco>().enabled = false;
        nextObjects.Add(a);
    }
}
