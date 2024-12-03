
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening; 

public class MainMenuManager : MonoBehaviour
{
    public GameObject infoPanel; // Tham chiếu đến Panel sẽ hiển thị
    public Button startButton;

    private void Start()
    {

        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }


        if (startButton != null)
        {
            startButton.transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void OnStartButtonClicked()
    {

        startButton.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            SceneManager.LoadScene(1);
            Debug.Log("Start Game");
        });
    }

    public void OnInfoButtonClicked()
    {
        if (infoPanel != null)
        {
            if (infoPanel.activeSelf)
            {

                infoPanel.transform.DOScale(0, 0.5f).OnComplete(() =>
                {
                    infoPanel.SetActive(false);
                });
            }
            else
            {
                infoPanel.SetActive(true);
                infoPanel.transform.localScale = Vector3.zero; // Đặt kích thước ban đầu
                infoPanel.transform.DOScale(1, 0.5f);
            }
        }
    }
}

