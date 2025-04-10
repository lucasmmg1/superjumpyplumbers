using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Manager : MonoBehaviour
{
    public PlatformerCharacter2D Player;
    public GameObject playerSpawner;

    Game GeneralGameSettings;

    private void Awake()
    {
        GeneralGameSettings = FindObjectOfType<Game>();
    }

    void Start()
    {
        PlatformerCharacter2D playerInstance = Instantiate(Player, playerSpawner.transform.position, Quaternion.identity);
        GeneralGameSettings.p_isAlive = true;
    }
    private void Update()
    {
        if (GeneralGameSettings.p_lives > 0 && !GeneralGameSettings.p_isAlive)
        {
            StartCoroutine(GeneralGameSettings.Respawn());
        }
    }
}
