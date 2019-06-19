using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sepay;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    private const float ENUM_SPEED = 4;

    [SerializeField] GameObject ball;
    [SerializeField] GameObject[] arrayOfPlayers;
    [SerializeField] GameObject[] spawnPoint;

    private int hostScore, awayScore;
    public bool isHost = true;
    public bool gameStarted;

    private float playerSpeed;

    public int HostScore { get => hostScore; set => hostScore = value; }
    public int AwayScore { get => awayScore; set => awayScore = value; }

    // Start is called before the first frame update
    void Start()
    {
        InitPlayers();
        if (PhotonNetwork.IsMasterClient)
        {
            arrayOfPlayers[0] = PhotonNetwork.Instantiate(arrayOfPlayers[0].name, spawnPoint[0].transform.position, Quaternion.identity);
        }
        else
        {
            arrayOfPlayers[1] = PhotonNetwork.Instantiate(arrayOfPlayers[1].name, spawnPoint[1].transform.position, Quaternion.identity);
        }
    }

    private void InitPlayers()
    {
        playerSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted == true)
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
        if (PhotonNetwork.IsMasterClient)
            arrayOfPlayers[0].GetComponent<Player>().Jump();
        else
            arrayOfPlayers[1].GetComponent<Player>().Jump();
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

        if (PhotonNetwork.IsMasterClient)
        {
            ResetPosition();
            arrayOfPlayers[1].GetComponent<PhotonView>().RPC("ResetPosition", RpcTarget.All);
        }
        else
        {
            ResetPosition();
            arrayOfPlayers[0].GetComponent<PhotonView>().RPC("ResetPosition", RpcTarget.All);
        }

        if (_homeGoal)
            hostScore += 1;
        else
            awayScore += 1;
    }

    [PunRPC]
    public void ResetPosition()
    {
        arrayOfPlayers[0].transform.position = new Vector3(-5, -1.3f);
        arrayOfPlayers[1].transform.position = new Vector3(5, -1.3f);
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
