using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ZombiesCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zombieCountText;

    SceneLoader sceneLoader;
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void Update()
    {
     
        int numberOfZombiesLeft = transform.childCount;
        if (numberOfZombiesLeft == 0)
        {
            sceneLoader.Win(); //si ya no queeda ningun zombie vivo, ve a la escena de win
        }
        zombieCountText.text = "No Muertos Restantes: " + numberOfZombiesLeft.ToString();
    }

}
