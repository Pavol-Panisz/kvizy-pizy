using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform birthplace;

    Dictionary<string, GameObject> addressPlayerDict = new Dictionary<string, GameObject>();
    List<GameObject> playerList = new List<GameObject>();

    public void ResetPositions()
    {
        //Debug.Log("called reset positions");
        foreach (GameObject pl in playerList)
        {
            Player plScript = pl.GetComponent<Player>();
            plScript.GoHome();
            //Debug.Log("set player " + pl.name + " home");
        }
    }

    public void CreatePlayer(string address)
    {
        GameObject go = GameObject.Instantiate(playerPrefab, birthplace.position, Quaternion.identity);

        Player goPlayerScript = go.GetComponent<Player>();
        
        addressPlayerDict.Add(address, go);
        playerList.Add(go);
        
        goPlayerScript.SetAddress(address);
        goPlayerScript.SetParent(birthplace);
    }

    public void AllPlayersHome() 
    {
        GameObject home = GameObject.FindGameObjectWithTag("Home");
        Transform docker = home.GetComponentInChildren<PlayerDocker>().transform;
        foreach (GameObject pl in playerList)
        {
            Player plScript = pl.GetComponent<Player>();
            plScript.ResetScore();

            plScript.SetParent(docker);
            plScript.Setup();
            plScript.SetCurrentParentAsHome();
        }
    }
}
