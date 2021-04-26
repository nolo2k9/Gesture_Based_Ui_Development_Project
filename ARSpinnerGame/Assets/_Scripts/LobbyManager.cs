using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField playerNameInputField;

    public GameObject ui_LoginGameObject;

    public GameObject ui_LobbyGameObject;

    public GameObject ui_3DGameObject;

    public GameObject ui_ConnectionStatusGameObject;

    public Text connectionStatus;

    public bool showConnectionStatus = false;



    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            //Only activate the lobbys UI
            ui_LobbyGameObject.SetActive(true);
            ui_3DGameObject.SetActive(true);

            ui_ConnectionStatusGameObject.SetActive(false);
            ui_LoginGameObject.SetActive(false);
        }
        else
        {
            //Only activate the login UI
            ui_LobbyGameObject.SetActive(false);
            ui_3DGameObject.SetActive(false);

            ui_ConnectionStatusGameObject.SetActive(true);
            ui_LoginGameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showConnectionStatus)
        {
            connectionStatus.text =
                "Connection Status: " + PhotonNetwork.NetworkClientState;
        }
    }


    public void OnEnterGameButtonClicked()
    {
        string playerName = playerNameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            ui_LobbyGameObject.SetActive(false);
            ui_3DGameObject.SetActive(false);
            ui_ConnectionStatusGameObject.SetActive(true);
            ui_LoginGameObject.SetActive(false);

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            Debug.Log("Player name is invalid or null");
        }
    }

    public void OnQuickMatchButtonClicked()
    {
        //SceneManager.LoadScene("Scene_Loading");
        SceneLoader.Instance.LoadScene("Scene_PlayerSelection");
    }







    public override void OnConnected()
    {
        Debug.Log("Connected to server");
    }

    public override void OnConnectedToMaster()
    {
        Debug
            .Log(PhotonNetwork.LocalPlayer.NickName +
            " is connected to photon server");

        ui_LobbyGameObject.SetActive(true);
        ui_3DGameObject.SetActive(true);
        ui_ConnectionStatusGameObject.SetActive(false);
        showConnectionStatus = true;
        ui_LoginGameObject.SetActive(false);
    }



}
