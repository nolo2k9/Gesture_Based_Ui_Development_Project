using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using TMPro;
using UnityEngine;

public class SetupPlayer : MonoBehaviourPun
{
    public TextMeshProUGUI nameText;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //Local player being controlled
            transform.GetComponent<MovementController>().enabled = true;

            //Get a handle on local players joystick object
            transform
                .GetComponent<MovementController>()
                .joystick
                .gameObject
                .SetActive(true);
        }
        else
        {
            //Network player disable the movement controller for that player
            transform.GetComponent<MovementController>().enabled = false;

            //Disable control of other players character
            transform
                .GetComponent<MovementController>()
                .joystick
                .gameObject
                .SetActive(false);
        }
        //Get the name
        GetPlayersName();
    }

    public void GetPlayersName()
    {
        //Access players Photon attributes
        if (nameText != null)
        {
            if (photonView.IsMine)
            {   //Display you over your character
                nameText.text = "YOU";
                nameText.color = Color.red;
            }
            else
            {
                //Otherwise remote player, display their name
                nameText.text = photonView.Owner.NickName;
            }
        }
    }
}
