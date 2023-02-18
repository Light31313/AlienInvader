using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private int increaseScoreAnimDuration = 3000;

    private void OnEnable()
    {
        IncreaseScoreAnim();
        Time.timeScale = 0f;
    }

    private async void IncreaseScoreAnim()
    {
        for (int i = 0; i <= playerStat.Score; i++)
        {
            scoreText.text = i.ToString();
            if(playerStat.Score > 0)
            {
                await Task.Delay(Mathf.RoundToInt((float)increaseScoreAnimDuration / playerStat.Score));
            }
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }


}
