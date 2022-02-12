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
    private int index;

    [Header("Debug")]
    [SerializeField] public Bloco atual;
    [SerializeField] public Bloco proximo;
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
                index = Random.Range(0, avaliableBlocks.Count - 1);
                spawnBlocks.Add(avaliableBlocks[index]);
            }
        }

        atual = spawnBlocks[0];
        proximo = spawnBlocks[1];
        GameObject obj = Instantiate(atual.gameObject, this.transform.position, Quaternion.identity);
        spawnBlocks.Remove(spawnBlocks[0]);
        
        index = Random.Range(0, avaliableBlocks.Count - 1);
        spawnBlocks.Add(avaliableBlocks[index]);


    }
    
}
