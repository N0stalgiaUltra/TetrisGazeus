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
        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        time += Time.deltaTime;
        if (time > .5f) 
        {
            if(CanMove())
            {
                //AddBlock();
                this.transform.position -= new Vector3(0, 1, 0);
                time = 0;
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                //GridManager.instance.LineControl();
                BlocoSpawner.instance.RandomSpawn();
                enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (CanMove())
                AddBlock();
           else
                transform.position += new Vector3(0, 0, 0);
        }
        
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (CanMove())
                AddBlock();
            else
                transform.position += new Vector3(0, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
             transform.position += new Vector3(0, -1, 0);

            if(CanMove())
                AddBlock();

            else
            {
                transform.position += new Vector3(0, 1, 0);
                //GridManager.instance.LineControl();
                BlocoSpawner.instance.RandomSpawn();
                enabled = false;
            }
        }


        if (Input.GetButtonDown("Rotate"))
            Rotation();

    }


    private void Rotation()
    {
        transform.eulerAngles += new Vector3(transform.rotation.x, transform.rotation.y ,-90);
    }
    
    public bool CanMove()
    {
        foreach (Transform childTransform in this.transform)
        {
            Vector2 childPosition = new Vector2((int)Mathf.Round(childTransform.position.x), (int)Mathf.Round(childTransform.position.y));


            if (!GridManager.instance.InBound(childPosition))
                return false;

            if (GridManager.grid[(int)childPosition.x, (int)childPosition.y] != null && GridManager.grid[(int)childPosition.x, (int)childPosition.y].parent != this.transform) //colidiu com alguém ?
                return false;

        }

        return true;
    }

    /// <summary>
    /// Salva a posição do bloco
    /// </summary>
    private void AddBlock()
    {
       /* não tá funcionando */
        
        for (int i = 0; i < GridManager.altura; i++)
            for (int j = 0; j < GridManager.largura; j++)
                if (GridManager.grid[i, j] != null)
                    if (GridManager.grid[i, j].parent == transform)
                        GridManager.grid[i, j] = null;
        
        //adiciona cada bloco a uma grid de transform, ou seja, adiciona ao grid a posição de um bloco
        foreach (Transform childTransform in gameObject.transform)
        {
            Vector2 childPosition = new Vector2((int)Mathf.Round(childTransform.position.x), (int)Mathf.Round(childTransform.position.y));

            if (GridManager.grid[(int)childPosition.x, (int)childPosition.y] == null) //checa se não há nenhum outro objeto ocupando a mesma pos
                GridManager.grid[(int)childPosition.x, (int)childPosition.y] = childTransform;

        }
    }
}
