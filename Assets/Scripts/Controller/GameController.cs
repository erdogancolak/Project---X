using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;
    public void StartGame()
    {
        Destroy(gameObject);
        Instantiate(PlayerGameObject);
        //Instantiate(EnemyGameObject);
        CameraFollower.isGameStart = true;
    }
}
