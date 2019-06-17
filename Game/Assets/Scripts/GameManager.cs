using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sepay;

public class GameManager : MonoBehaviour
{
    private const float ENUM_SPEED = 4;

    [SerializeField] GameObject ball;
    [SerializeField] GameObject[] arrayOfPlayers;

    public Transform[] spawnPoint;

    private int hostScore, awayScore;
    //public bool isHost = true;
    public bool gameStarted;

    private float playerSpeed;

    public int HostScore { get => hostScore; set => hostScore = value; }
    public int AwayScore { get => awayScore; set => awayScore = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            arrayOfPlayers[0] = PhotonNetwork.Instantiate(arrayOfPlayers[0].name, spawnPoint[0].position, Quaternion.identity);
        }
        else
        {
            arrayOfPlayers[1] = PhotonNetwork.Instantiate(arrayOfPlayers[1].name, spawnPoint[1].position, Quaternion.identity);
        }
        InitPlayers();
    }

    private void InitPlayers()
    {
        playerSpeed = 0;
        //arrayOfPlayers[0].GetComponent<Player>().IsHostPlayer = true;
        //arrayOfPlayers[0].GetComponent<Player>().IsHost = isHost;
        //arrayOfPlayers[1].GetComponent<Player>().IsHostPlayer = false;
        //arrayOfPlayers[1].GetComponent<Player>().IsHost = isHost;
    }

    // Update is called once per frame
    void Update()
    {
        //GetInput();
        if (gameStarted)
        {
            UpdatePlayer();
        }
        
    }

    private void UpdatePlayer()
    {
        MovePlayer(Vector2.right * playerSpeed);
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

    public void OnLeftButtonDown()
    {
        playerSpeed = ENUM_SPEED * -1;
    }
    public void OnLeftButtonUp()
    {
        playerSpeed = 0;
    }
    public void OnJumpButtonDown()
    {
        Jump();
    }
    public void OnRightButtonDown()
    {
        playerSpeed = ENUM_SPEED;
    }
    public void OnRightButtonUp()
    {
        playerSpeed = 0;
    }
    public void OnFlatKickButtonDown()
    {
        if (PhotonNetwork.IsMasterClient)
            arrayOfPlayers[0].GetComponent<Player>().FlatKick();
        else
            arrayOfPlayers[1].GetComponent<PhotonView>().RPC("FlatKick", RpcTarget.All);
    }
    public void OnLobKickButtonDown()
    {
        if (PhotonNetwork.IsMasterClient)
            arrayOfPlayers[0].GetComponent<Player>().LobKick();
        else
            arrayOfPlayers[1].GetComponent<PhotonView>().RPC("LobKick", RpcTarget.All);
    }
}
