using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouWinScript : MonoBehaviour
{
    public string playerName;
    public TextMeshProUGUI textDisplayName;

    private void Awake()
    {
        playerName = NameTransfer.instance.playerName;
    }

    public void DisplayNameAndHealth()
    {
        textDisplayName.text = "Recolector " + playerName;
    }
}
