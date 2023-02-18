using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerStat stat;
    [SerializeField]
    private GameObject playMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    private void Awake()
    {
        stat.ResetStat();
    }

    public async void ShowGameOverMenu(object data)
    {
        await Task.Delay(2000);
        gameOverMenu.SetActive(true);
    }

}
