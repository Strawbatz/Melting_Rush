using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSreen : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] GameObject escaped;
    [SerializeField] TextMeshProUGUI iceLeft;
    [SerializeField] TextMeshProUGUI timeTaken;
    [SerializeField] GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
        container.SetActive(false);
        EndPoint.instance.onEndLevel += LevelComplete;
        LeanTween.moveLocalX(escaped, -1600, 0f).setIgnoreTimeScale(true);
        LeanTween.moveLocalX(iceLeft.gameObject, -1700, 0f).setIgnoreTimeScale(true);
        LeanTween.moveLocalX(timeTaken.gameObject, -1700, 0f).setIgnoreTimeScale(true);
        LeanTween.moveLocalX(buttons, -1600, 0f).setIgnoreTimeScale(true);
    }

    private void LevelComplete() {
        Melting melting = FindObjectOfType<Melting>();
        container.SetActive(true);

        SoundManager.instance.PlaySound(SoundManager.Sound.Complete);

        LeanTween.moveLocalX(escaped, 0, 1f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);

        iceLeft.text = (melting.GetIceLeft()*100).ToString("F0") + "% Ice left!";
        LeanTween.moveLocalX(iceLeft.gameObject, 0, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);

        timeTaken.text = "Time: " + Time.time.ToString("F2");
        LeanTween.moveLocalX(timeTaken.gameObject, 0, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);

        LeanTween.moveLocalX(buttons, 0, 1f).setDelay(0.2f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit() {
        Time.timeScale = 1;
        Application.Quit();
    }
}
