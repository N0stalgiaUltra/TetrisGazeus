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
            if (grid[largura, alt] == null)
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
            if(FullLines(i))
            {
                CleanLines(i);
                for (int j = altura; j < altura; j++) //loopa para pegar as linhas acima da deletada e descer elas
                    DropLines(i);
                i--;
            }
                
        }
    }

    /// <summary>
    /// Retorna se o bloco está dentro do limite da grid
    /// </summary>
    /// <returns></returns>
    public bool InBound(Vector2 position)
    { 
        int posx = (int)Math.Round(position.x);
        int posy = (int)Math.Round(position.y);

        if (posx >= 0 && posx < largura && posy > 0)
            return true;

        return false;
    }
}
