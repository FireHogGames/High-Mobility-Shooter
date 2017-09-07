using UnityEngine;

public class Home : MonoBehaviour {

    [Header("Tabs")]
    public GameObject playOnline;
    public GameObject settings;
    public GameObject news;
    public GameObject stats;

    public void PlayOnline()
    {
        playOnline.SetActive(true);
        settings.SetActive(false);
        news.SetActive(false);
        stats.SetActive(false);
    }

    public void Settings()
    {
        playOnline.SetActive(false);
        settings.SetActive(true);
        news.SetActive(false);
        stats.SetActive(false);
    }

    public void News()
    {
        playOnline.SetActive(false);
        settings.SetActive(false);
        news.SetActive(true);
        stats.SetActive(false);
    }

    public void Stats()
    {
        playOnline.SetActive(false);
        settings.SetActive(false);
        news.SetActive(false);
        stats.SetActive(true);
    }
    public void CloseTab()
    {
        playOnline.SetActive(false);
        settings.SetActive(false);
        news.SetActive(false);
        stats.SetActive(false);
    }
}
