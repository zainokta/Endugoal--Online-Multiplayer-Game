using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "LeftGoal")
        {
            Debug.Log("Away Goal");
            gameManager.GetComponent<GameManager>().Goal(true);
        }

        if (_collision.gameObject.tag == "RightGoal")
        {
            Debug.Log("Home Goal");
            gameManager.GetComponent<GameManager>().Goal(false);
        }
    }
}
