using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{

    private Vector2 movimento;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Rotate"))
            Rotation();

    }


    private void Rotation()
    {
        print("rotacionando o objeto");
    }
}
