using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Singleton
    public static GridManager instance;
    #endregion


    [Header("Info da Grid")]
    public static int altura = 20;
    public static int largura = 10;

    public static Transform[,] grid = new Transform[largura, altura];

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
    private void CleanLines(int alt)
    {
        for (int i = 0; i < largura; i++)
        {
            Destroy(grid[i, alt].gameObject);
            grid[i, alt] = null;

            ////drop de linhas
            //int auxAlt = alt+1; 
            //if (grid[i, auxAlt] != null)
            //{
            //    print($"drop fora {i}");
            //    grid[i, alt] = grid[i, auxAlt];
            //    grid[i, auxAlt] = null;
            //    grid[i, alt].position += new Vector3(0, -1, 0);
            //}
        }
        
    }

    /// <summary>
    /// Desce as linhas acima daquela que foi resetada
    /// </summary>
    /// <param name="altura">Altura da grid acima da que foi resetada</param>
    private void DropLines(int alt)
    {
        for (int j = alt; j < altura; j++)
        {
            for (int i = 0; i < largura; i++)
            {
                if (grid[i, j] != null)
                {
                    print($"drop lines{i}, row{j}");
                    grid[i, j- 1] = grid[i, j];
                    grid[i, j] = null;
                    
                    if(grid[i, j - 1].parent.rotation.z == 90)
                        grid[i, j - 1].position += new Vector3(-1, 0, 0);
                    else if(grid[i, j - 1].parent.rotation.z == -90)
                        grid[i, j - 1].position += new Vector3(-1, 0, 0);
                    else
                        grid[i, j- 1].position += new Vector3(0, -1, 0); 
                }
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
            if (grid[i, alt] == null)
                return false;
         return true;
    }
    /// <summary>
    /// Determina o controle das linhas, ou seja, quando limpar e acertar a posição delas.
    /// </summary>
    public void LineControl()
    {
        for (int i = 0; i < altura; i++)
        {
            if (FullLines(i))
            {
                CleanLines(i);
                DropLines(i);
                i--;
            }                
        }
    }

   
}
