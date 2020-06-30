using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = null;

    private void Start()
    {
        if (playerManager) { CreateFakePlayers(); }
    }

    private void CreateFakePlayers()
    {
        playerManager.CreatePlayer("00001");
        playerManager.CreatePlayer("00002");
        playerManager.CreatePlayer("00003");
        playerManager.CreatePlayer("00004");
        playerManager.CreatePlayer("00005");
    }
}
