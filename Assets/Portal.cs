using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: Portal.cs
/// History: 
/// 2020-09-30  - For transferring the player
///             - not using for now.
///             
/// Last Modified: 2020-09-30
/// </summary>

public class Portal : MonoBehaviour
{
    #region Variables

    [SerializeField]
    string transferMapName;

    private CharacterController player;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //when player trigger the collider.
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "Player")
        {
           // player.currentMapName = transferMapName;
            //change scene.
            SceneManager.LoadScene(transferMapName);
        }
    }
}
