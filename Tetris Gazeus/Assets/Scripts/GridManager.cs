using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Singleton
    public static GridManager instance;
    #endregion


    [Header("Info da Grid")]
    [SerializeField] private int altura = 20;
    [SerializeField] private int largura = 10;

    private static Transform[,] grid = new Transform[instance.largura, instance.altura];

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

    /// <summary>
    /// Limpa as linhas completadas, resetando-as
    /// </summary>
    /// <param name="alt"> Altura da grid a ser resetada </param>
    public void CleanLines(int alt)
    {
        for (int i = 0; i < largura; i++)
        {
            Destroy(grid[largura, alt].gameObject);
            grid[largura, alt] = null;
        }
        
    }

    /// <summary>
    /// Desce as linhas acima daquela que foi resetada
    /// </summary>
    /// <param name="altura">Altura da grid acima da que foi resetada</param>
    public void DropLines(int alt)
    {
        for (int i = 0; i < largura; i++)
        {
            if(grid[largura,alt] != null)
            {
                grid[largura, alt - 1] = grid[largura, alt];
                grid[largura, alt] = null;
                grid[largura, alt - 1].position += new Vector3(0,-1,0);
            }
        }
    }
    /// <summary>
    /// Checa se as linhas estão preenchidas
    /// </summary>
    /// <param name="alt">altura da grid</param>
    /// <returns>se a linha está preenchida</returns>
    private bool FullLines(int alt)
    {
        for (int i = 0; i < largura; i++)
            if (grid[largura, alt] != null)
                return true;
        return false;
    }

    public void LineControl()
    {
        for (int i = 0; i < instance.altura; i++)
        {
            if(FullLines(i))
            {
                CleanLines(i);
                for (int j = instance.altura; j < instance.altura; j++) //loopa para pegar as linhas acima da deletada e descer elas
                    DropLines(i);
            }
                
        }
    }

    #region Propriedades
    public int GridAltura { get => altura; }
    public int GridLargura { get => largura; }
    #endregion
}
