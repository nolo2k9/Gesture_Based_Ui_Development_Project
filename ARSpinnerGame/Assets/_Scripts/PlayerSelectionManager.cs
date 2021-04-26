using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectionManager : MonoBehaviour
{
    //Switch between players
    public Transform playerSwitcherTransform;

    //Next Button
    public Button button_next;

    //Previous button
    public Button button_last;

    //player selection number (0-4)
    public int playerSelection;

    // Playable characters
    public GameObject[] spinners;

    //Show if player is attack or defensive type
    public TextMeshProUGUI playerType;

    //Selection game object
    public GameObject ui_Selection;

    //After Selection game object
    public GameObject ui_AfterSelection;

    // Start is called before the first frame update
    void Start()
    {
        //Show first charcater in the list at the start
        playerSelection = 0;

        //Show Selection panel with needed buttons
        ui_Selection.SetActive(true);

        //Hide panel shown after player selection has been made
        ui_AfterSelection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextPlayer()
    {
        //Increment player selection by 1
        playerSelection += 1;

        if (playerSelection >= spinners.Length)
        {
            //end of array
            playerSelection = 0;
        }
        Debug.Log (playerSelection);

        //disabled until round has been finished
        button_next.enabled = false;
        button_last.enabled = false;

        //1 second to make turn between players
        StartCoroutine(Rotate(Vector3.up, playerSwitcherTransform, 90, 1.0f));

        if (playerSelection == 0 || playerSelection == 1)
        {
            //The type of player is an attacking type
            playerType.text = "Attacker";
        }
        else
        {
            //The type of player is an defending type
            playerType.text = "Defender";
        }
    }

    //Method to use revious button
    public void LastPlayer()
    {
        //Decrement

        playerSelection -= 1;
        if (playerSelection < 0)
        {
            //end of array
            playerSelection = spinners.Length - 1;
        }
        Debug.Log (playerSelection);

        //Dont show until full cycle has been finished
        button_next.enabled = false;
        button_last.enabled = false;

        //1 second to make turn between players
        StartCoroutine(Rotate(Vector3.up, playerSwitcherTransform, -90, 1.0f));
        if (playerSelection == 0 || playerSelection == 1)
        {
            //The type of player is an attacking type
            playerType.text = "Attacker";
        }
        else
        {
            //The type of player is an defending type
            playerType.text = "Defender";
        }
    }

    public void SelectButton()
    {
        ui_Selection.SetActive(false);
        ui_AfterSelection.SetActive(true);

        //Set a custom properties to players
        ExitGames.Client.Photon.Hashtable selectionProp =
            new ExitGames.Client.Photon.Hashtable {
                {
                    MultiplayerARSpinnerTopGame.PLAYER_SELECTION_NUMBER,
                    playerSelection
                }
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties (selectionProp);
    }

    //Re-select button pressed
    public void ReSelectButton()
    {
        ui_Selection.SetActive(true);
        ui_AfterSelection.SetActive(false);
    }

    //Battle button load new scene
    public void BattleButton()
    {
        SceneLoader.Instance.LoadScene("Scene_Gameplay");
    }

    //Go back and load previous scene

    public void BackButton()
    {
        SceneLoader.Instance.LoadScene("Scene_Lobby");
    }

    IEnumerator
    Rotate(
        Vector3 axis,
        Transform transformToRotate,
        float angle,
        float duration = 1.0f
    )
    {
        Quaternion originalRotation = transformToRotate.rotation;
        Quaternion finalRotation =
            transformToRotate.rotation * Quaternion.Euler(axis * angle);

        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            transformToRotate.rotation =
                Quaternion
                    .Slerp(originalRotation,
                    finalRotation,
                    elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transformToRotate.rotation = finalRotation;
        button_next.enabled = true;
        button_last.enabled = true;
    }
}
