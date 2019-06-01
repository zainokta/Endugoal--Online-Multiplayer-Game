using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sepay;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] arrayOfPlayers;
    private int hostScore, awayScore;
    public bool isHost = true;
    [SerializeField] GameObject ball;
    Button btn;

    public int HostScore { get => hostScore; set => hostScore = value; }
    public int AwayScore { get => awayScore; set => awayScore = value; }

    // Start is called before the first frame update
    void Start()
    {
        InitPlayers();
    }

    private void InitPlayers()
    {
        arrayOfPlayers[0].GetComponent<Player>().IsHostPlayer = true;
        arrayOfPlayers[0].GetComponent<Player>().IsHost = isHost;
        arrayOfPlayers[1].GetComponent<Player>().IsHostPlayer = false;
        arrayOfPlayers[1].GetComponent<Player>().IsHost = isHost;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayer(Vector2.left * 4);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayer(Vector2.right * 4);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if(PhotonNetwork.IsMasterClient)
            arrayOfPlayers[0].GetComponent<Rigidbody2D>().velocity = new Vector2(arrayOfPlayers[0].GetComponent<Rigidbody2D>().velocity.x, 10);
        else
            arrayOfPlayers[1].GetComponent<Rigidbody2D>().velocity = new Vector2(arrayOfPlayers[1].GetComponent<Rigidbody2D>().velocity.x, 10);
    }

    private void MovePlayer(Vector2 _velocity)
    {
        if (PhotonNetwork.IsMasterClient)
            arrayOfPlayers[0].GetComponent<Rigidbody2D>().velocity = new Vector2(_velocity.x, arrayOfPlayers[0].GetComponent<Rigidbody2D>().velocity.y);
        
        else
            arrayOfPlayers[1].GetComponent<Rigidbody2D>().velocity = new Vector2(_velocity.x, arrayOfPlayers[1].GetComponent<Rigidbody2D>().velocity.y);
        
    }

    public void Goal(bool _homeGoal)
    {
        ball.transform.position = new Vector3(0, 3);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0;

        arrayOfPlayers[0].transform.position = new Vector3(-5, -1.3f);
        arrayOfPlayers[1].transform.position = new Vector3(5, -1.3f);

        if (_homeGoal)
            hostScore += 1;
        else
            awayScore += 1;
    }
}
