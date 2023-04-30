using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject Player;
    public Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Debug.LogError("No player set to spawn");
            return;
        }

        GameObject player = Instantiate(Player);
        player.transform.position = spawnPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
