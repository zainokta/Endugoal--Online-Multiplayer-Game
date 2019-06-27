﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public Button battleButton;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings(); //Connect to Photon Server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player Connected to Server");
        battleButton.interactable = true;
    }


    public void CreateRoom()
    {
        RoomOptions roomOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        int room = Random.Range(0, 1000);
        PhotonNetwork.CreateRoom("Room " + room.ToString(), roomOpts);

    }

    public void JoinRoom()
    {
        Debug.Log("Joining into the room...");
        battleButton.interactable = false;

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Timeout Started");
        StartCoroutine(Timeout());
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(3f);
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        StopAllCoroutines();
        Debug.Log("On room " + PhotonNetwork.CurrentRoom);
        PhotonNetwork.LoadLevel("Core");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {

    }
}
