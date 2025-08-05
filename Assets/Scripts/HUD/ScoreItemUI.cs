using UnityEngine;

public class ScoreItemUI : MonoBehaviour
{
    [SerializeField]TMPro.TextMeshProUGUI playerNameText;
    [SerializeField]TMPro.TextMeshProUGUI playerScoreText;
    
    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }
    public void SetPlayerScore(int score)
    {
        playerScoreText.text = score.ToString();
    }
    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
    public void TurnOn()
    {
        gameObject.SetActive(true);
    }
}
