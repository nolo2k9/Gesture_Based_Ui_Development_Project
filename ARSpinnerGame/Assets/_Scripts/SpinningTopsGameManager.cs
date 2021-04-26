using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SpinningTopsGameManager :MonoBehaviourPunCallbacks
{
    // Information panel game object
    public GameObject informationPanel;
    // Information panel game object text
    public TextMeshProUGUI informationText;
    //Search or games button
    public GameObject searchForGamesButtonGameobject;
    public GameObject adjust_Button;
    public GameObject raycastCenter_Image;

    // Start is called before the first frame update
    void Start()
    {   //Activate information panel
        informationPanel.SetActive(true);
        informationText.text = "Search For Games.....";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //https://doc.photonengine.com/en-us/realtime/current/lobby-and-matchmaking/matchmaking-and-lobby
    //https://doc-api.photonengine.com/en/pun/v2/class_photon_1_1_pun_1_1_mono_behaviour_pun_callbacks.html
    public void JoinRandomRoom()
    {
        informationText.text = "Searching for available rooms...";
        //Join a random room
        PhotonNetwork.JoinRandomRoom();
        //Deactivate button
        searchForGamesButtonGameobject.SetActive(false);
    }

    //Exit out of the room
    //https://doc.photonengine.com/en-us/realtime/current/lobby-and-matchmaking/matchmaking-and-lobby
    //https://doc-api.photonengine.com/en/pun/v2/class_photon_1_1_pun_1_1_mono_behaviour_pun_callbacks.html

    public void QuitButton()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {   //Load lobby scene
            SceneLoader.Instance.LoadScene("Scene_Lobby");
        }      
    }

    //If joining a room has failed
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //DIsplay error to console
        Debug.Log(message);
        //Print message
        informationText.text = message;
        //Try again
        CreateAndJoin();
    }

    public override void OnJoinedRoom()
    {
        adjust_Button.SetActive(false);
        raycastCenter_Image.SetActive(false);

        //If there is only 1 other player in the room
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //display message
            informationText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name + ". Waiting for other players...";
        }
        else
        {   //display message
            informationText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name;
            //deactiavte information panel
            StartCoroutine(DeactivateAfterSeconds(informationPanel, 2.0f));
        }
        //Debug
        Debug.Log( " joined to "+ PhotonNetwork.CurrentRoom.Name);
    }

    /*
        When player eneters a room.
        Display a message and deactivate after 2 seconds
    */
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {      
        Debug.Log(newPlayer.NickName + " joined to "+ PhotonNetwork.CurrentRoom.Name+ " Player count "+ PhotonNetwork.CurrentRoom.PlayerCount);
        informationText.text = newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount;

        StartCoroutine(DeactivateAfterSeconds(informationPanel, 2.0f));
    }

    //When the lobby has been left load lobby scene
   public override void OnLeftRoom()
    {
        SceneLoader.Instance.LoadScene("Scene_Lobby");
    }

    void CreateAndJoin()
    {
        //Choose a random room between 0 and 1000
        string roomName = "Room" + Random.Range(0,1000);
        //Set max players
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        //Creating the room passing name and options
        PhotonNetwork.CreateRoom(roomName,options);
    }
    //Deactiavte panels
    IEnumerator DeactivateAfterSeconds(GameObject _gameObject, float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        _gameObject.SetActive(false);
    }
}
