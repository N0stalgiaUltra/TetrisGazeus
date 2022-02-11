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
        //dessa forma o movimento fica mais proximo do tetris de console
        if(InBound())
        {
            //a cada 0.5s, um bloco cai 
            if (time > .5f)
            {
                this.transform.position -= new Vector3(0, 1, 0);
                time = 0;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position += new Vector3(1, 0, 0);
            
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position += new Vector3(-1, 0, 0);
            
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position += new Vector3(0, -1, 0);
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

    /// <summary>
    /// Retorna se o bloco está dentro do limite da grid
    /// </summary>
    /// <returns></returns>
    private bool InBound()
    {
        foreach (Transform childTransform in gameObject.transform)
        {
            if (childTransform.position.x >= (GridManager.instance.GridLargura / 2) || childTransform.position.x <= -(GridManager.instance.GridLargura / 2)|| childTransform.position.y < -(GridManager.instance.GridAltura/2))
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
