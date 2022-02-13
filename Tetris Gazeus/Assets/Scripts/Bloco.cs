using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float time;
    private static Transform[,] grid;

    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (CanMove()) { }
            //AddBlock();
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (CanMove()){}
            else
            {
                transform.position += new Vector3(-1, 0, 0);
                //AddBlock();
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || time > .5f)
        {
             transform.position += new Vector3(0, -1, 0);

            if(!CanMove()){
                transform.position += new Vector3(0, 1, 0);
                //GridManager.instance.LineControl();
                AddBlock();
                BlocoSpawner.instance.RandomSpawn();

            }
            time = 0;
        }


        if (Input.GetButtonDown("Rotate"))
            Rotation();
        

    }

    /// <summary>
    /// Rotaciona os blocos
    /// </summary>
    /* Precisa de correções no centro dos blocos */
    private void Rotation()
    {
        if(!CanMove())
            transform.eulerAngles += new Vector3(transform.rotation.x, transform.rotation.y ,-90);
        else
            transform.eulerAngles += new Vector3(transform.rotation.x, transform.rotation.y, 90);

    }

    /// <summary>
    /// Checa se o bloco ainda pode se movimentar
    /// </summary>
    /// <returns> se pode ou não </returns>
    public bool CanMove()
    {
        foreach (Transform childTransform in transform)
        {
            Vector2 childPosition = new Vector2((int)Mathf.Round(childTransform.position.x), (int)Mathf.Round(childTransform.position.y));


            if (!InBound(childPosition))
                return false;

            if (GridManager.grid[(int)childPosition.x, (int)childPosition.y] != null && GridManager.grid[(int)childPosition.x, (int)childPosition.y].parent != transform) //colidiu com alguém ?
            {
                
                AddBlock();
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Retorna se o bloco está dentro do limite da grid
    /// </summary>
    /// <returns></returns>
    public bool InBound(Vector2 position)
    {
        int posx = (int)Math.Round(position.x);
        int posy = (int)Math.Round(position.y);

        if (posx >= 0 && posx < GridManager.largura && posy > 0)
            return true;

        return false;
    }
    
    /// <summary>
    /// Salva a posição do bloco na grid
    /// </summary>
    private void AddBlock()
    {
        //adiciona cada bloco a uma grid de transform, ou seja, adiciona ao grid a posição de um bloco

        int count = 0;
        while (count <= 3)
        {
            foreach (Transform childTransform in transform)
            {
                Vector2 childPosition = new Vector2((int)Mathf.Round(childTransform.position.x), (int)Mathf.Round(childTransform.position.y));

                if (GridManager.grid[(int)childPosition.x, (int)childPosition.y] == null)
                {
                    GridManager.grid[(int)childPosition.x, (int)childPosition.y] = childTransform;
                    //print(count);
                    //print($"adicionado bloco na pos {(int)childPosition.x}, {(int)childPosition.y}");

                }

                count++;
            }
        }
        enabled = false;
    }

    IEnumerator DisableBlock()
    {
        
        yield return new WaitForSeconds(.5f);
        enabled = false;
    }
}
