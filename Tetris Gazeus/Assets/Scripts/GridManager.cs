using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Singleton
    public static GridManager instance;
    #endregion


    [Header("Info da Grid")]
    [SerializeField] private int altura;
    [SerializeField] private int largura;

    public static int[,] grid;

    private void Awake()
    {
        //declarando Singleton
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

    }

    private void Update()
    {
    }
    private void CleanLines()
    {
        //loopa a grid na altura
            //checa se há alguma linha == null
                //se tiver: deleta todos os objs
                    // chama metodo pra descer a linha
                //senão, i++
    }

    private void DropLines()
    {
        //loopa a grid pela altura
            //checa qual a linha que ficou null
            //pega a linha acima e desce
            //faz isso para todas as linhas
    }
    #region Propriedades
    public int GridAltura { get => altura; }
    public int GridLargura { get => largura; }
    #endregion
}
