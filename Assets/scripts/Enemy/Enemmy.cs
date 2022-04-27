using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemmy : MonoBehaviour
{
    private bool checkTrigger;
    [SerializeField]private float speed;
    [SerializeField]private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Char").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        checkTrigger = true;

        if (checkTrigger)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Char")
        {
            checkTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Char")
        {
            checkTrigger = false;
        }
    }

}





