using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

namespace myNamespace {
    public class Launcher : MonoBehaviourPunCallbacks
    {

        bool isConnecting;

        [SerializeField]
        private GameObject playPanel;

        [SerializeField]
        private GameObject loadingPanel;

        string gameVersion = "1";

        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true; // odadaki herkesin ayný sahnede olmasýný saðlýyor.
        }
        void Start()
        {
            loadingPanel.SetActive(false);
            playPanel.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Connect()
        {

            

            // baðlý olup olmadýðýmýzý kontrol ediyoruz baðlýysa rastgele bir odaya giriyor, deðilse baðlantý saðlýyor.
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }

            loadingPanel.SetActive(true);
            playPanel.SetActive(false);

        }

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                
                // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            loadingPanel.SetActive(false);
            playPanel.SetActive(true);

            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                {
                    Debug.Log("We load the 'Room' ");

                    // #Critical
                    // Load the Room Level.
                    PhotonNetwork.LoadLevel("Room");
                }
            }
             
            
        }

    }
}

