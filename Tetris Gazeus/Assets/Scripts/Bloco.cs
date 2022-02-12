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
                this.transform.position -= new Vector3(0, 1, 0);
                time = 0;
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                GridManager.instance.LineControl();
                BlocoSpawner.instance.RandomSpawn();
                enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           if(CanMove())
                transform.position += new Vector3(-1, 0, 0);
           else
                transform.position += new Vector3(1, 0, 0);
        }
        
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           if(CanMove())
                transform.position += new Vector3(1, 0, 0);
           else
                transform.position += new Vector3(-1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(CanMove())
                transform.position += new Vector3(0, -1, 0);
            else
            {
                transform.position += new Vector3(0, 1, 0);
                GridManager.instance.LineControl();
                BlocoSpawner.instance.RandomSpawn();
                enabled = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.A))
            AddBlock();

        if (Input.GetButtonDown("Rotate"))
            Rotation();

    }


    private void Rotation()
    {
        print("rotacionando o objeto");
    }
    
    public bool CanMove()
    {
        foreach (Transform childTransform in this.transform)
        {
            Vector2 childPosition = new Vector2((int)Mathf.Round(childTransform.position.x), (int)Mathf.Round(childTransform.position.y));


            if (!GridManager.instance.InBound(childPosition))
                return false;

            if (GridManager.grid[(int)childPosition.x, (int)childPosition.y] != null) //colidiu com alguém ?
                return false;
        }

        return true;
    }

    /// <summary>
    /// Salva a posição do bloco em uma grid 
    /// </summary>
    private void AddBlock()
    {   
        //adiciona cada bloco a uma grid de transform, ou seja, adiciona ao grid a posição de um bloco
        foreach (Transform childTransform in gameObject.transform)
        {
            int x = (int)Math.Round(childTransform.position.x);
            int y = (int)Math.Round(childTransform.position.y);

            if (grid[x, y] == null) //checa se não há nenhum outro objeto ocupando a mesma pos
                grid[x, y] = childTransform;

        }
    }
}
