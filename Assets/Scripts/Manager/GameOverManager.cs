using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public bool isDead;

    [SerializeField] private TextMeshProUGUI _bannerText;
    [SerializeField] private GameObject _gameOverPopup;

    void Start()
    {
        _gameOverPopup.SetActive(true);

        if (isDead)
        {
            _bannerText.text = "패배!";
        }
        else
        {
            _bannerText.text = "승리!";
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnClickEnd()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}   
