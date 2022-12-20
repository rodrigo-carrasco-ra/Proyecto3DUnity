using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NameTransfer : MonoBehaviour
{
    public static NameTransfer instance;
    public string playerName ;
    public string playerAge;
    public GameObject inputFieldPlayer;
    public GameObject inputFieldAge;
    public GameObject textDisplayName;
    public GameObject textDisplayAge;
    byte age;
    public bool isNumber;
    float timeToStart = 2f;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StoreName()
    {
        playerName = inputFieldPlayer.GetComponent<Text>().text;
        playerAge = inputFieldAge.GetComponent<Text>().text;
        isNumber = byte.TryParse(playerAge, out age);
        if (playerName != null && age >= 18) {
            StartCoroutine(ShowNameAndAge());

        }
        else if (isNumber == false )
        {
            Debug.Log("Ingresa un numero");
        }
        else
        {
            Debug.Log($"{playerName}, eres muy pequeño");
        }

    }
    IEnumerator ShowNameAndAge()
    {
        textDisplayName.GetComponent<Text>().text = "Recolector " + playerName;
        textDisplayAge.GetComponent<Text>().text = "Edad " + playerAge;
        yield return new WaitForSeconds(timeToStart);
        SceneManager.LoadScene("MainScene");
    }


}
