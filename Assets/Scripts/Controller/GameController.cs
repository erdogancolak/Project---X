using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("References")]

    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;

    public GameObject StartCanvas;
    public void StartGame()
    {
        Destroy(StartCanvas);
        Instantiate(PlayerGameObject);
        //Instantiate(EnemyGameObject);
        CameraFollower.isGameStart = true;
    }
}
