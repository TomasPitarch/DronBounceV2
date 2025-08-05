using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnPoints;

    [SerializeField]
    private List<GameObject> cameraSpawnPoints;

   
    private Camera _myCamera;

    [SerializeField]
    private string playerPrefabName;

    [SerializeField]
    private PlayerController playerController;
    
    private TypeOfRoom _typeOfRoom;

    private void Start()
    {
        _myCamera = Camera.main;
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public void SpawnPlayer(PlayerOrder playerOrder)
    {
        int order = PlayerOrderToIndex(playerOrder);

        Quaternion rotation = new Quaternion();

        rotation.eulerAngles = spawnPoints[order].transform.eulerAngles;
        
        GameObject character = PhotonNetwork.Instantiate(playerPrefabName,
            spawnPoints[order].transform.position,
            rotation);



        _myCamera.transform.position = cameraSpawnPoints[order].transform.position;
        rotation.eulerAngles = cameraSpawnPoints[order].transform.eulerAngles;
        _myCamera.transform.eulerAngles = rotation.eulerAngles;


        playerController.SetCharacter(character.GetComponent<Character>());
    }

    public void SetTypeOfRoom(TypeOfRoom typeOfRoom)
    {
        _typeOfRoom = typeOfRoom;
    }
  
    private int PlayerOrderToIndex(PlayerOrder playerOrder)
    {
        if (_typeOfRoom == TypeOfRoom.PvP && playerOrder == PlayerOrder.Player2)
        {
            return (int)playerOrder + 1; 
        }
        
        return (int)playerOrder;
        
    }
    

}
