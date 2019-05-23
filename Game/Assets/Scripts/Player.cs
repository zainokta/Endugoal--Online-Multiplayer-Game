using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sepay
{
    public class Player : MonoBehaviour
    {
        private bool isHost;
        private bool isHostPlayer;

        public bool IsHost { get => isHost; set => isHost = value; }
        public bool IsHostPlayer { get => isHostPlayer; set => isHostPlayer = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            //Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Ball")
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    Debug.Log("Z Press and collide");
                    if (IsHost && IsHostPlayer && collision.gameObject.transform.position.x > transform.position.x)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.1f) * 500);
                        Debug.Log("Sepak");
                    }

                    if (!IsHost && !IsHostPlayer && collision.gameObject.transform.position.x < transform.position.x)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.1f) * 500);
                    }
                }

                if (Input.GetKey(KeyCode.X))
                {
                    Debug.Log("X Press and collide");
                    if (IsHost && IsHostPlayer && collision.gameObject.transform.position.x > transform.position.x)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 1) * 500);
                        Debug.Log("Sepak munggah");
                    }

                    if (!IsHost && !IsHostPlayer && collision.gameObject.transform.position.x < transform.position.x)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 1) * 500);
                    }
                }
            }
        }
    }

}