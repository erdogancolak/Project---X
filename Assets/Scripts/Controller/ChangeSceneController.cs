using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private string sceneName;

    [SerializeField] private string playerTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            DontDestroyOnLoad(collision.gameObject);
            collision.gameObject.transform.position = new Vector2(-6, 1.55f);
            SceneManager.LoadScene(sceneName);
        }
    }
}
