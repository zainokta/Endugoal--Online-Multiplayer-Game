using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Sepay
{
    public class Player : MonoBehaviour
    {
        private bool canKick = false, canJump = false;

        [SerializeField] GameObject ball;


        // Start is called before the first frame update
        void Start()
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Field")
            {
                canJump = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Field")
            {
                canJump = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                if ((PhotonNetwork.IsMasterClient) || (!PhotonNetwork.IsMasterClient))
                {
                    canKick = true;
                    Debug.Log("Can Kick = " + canKick);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                canKick = false;
                Debug.Log("Can Kick = " + canKick);
            }
        }

        public void Jump()
        {
            if (canJump)
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10);
        }

        [PunRPC]
        public void FlatKick()
        {
            if (canKick)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.1f) * 500);
                }
                else
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.1f) * 500);
                }
            }
        }

        [PunRPC]
        public void LobKick()
        {
            if (canKick)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 1) * 500);
                }
                else
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 1) * 500);
                }
            }
        }
    }

}
