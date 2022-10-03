using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject launcherGroup;
    [SerializeField] private Image loading;
    [SerializeField] private float speed;
    private float _timer;
    private bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.CreateSaveFolder();
        AudioManager.instance.Play(AudioManager.AudioList.SFX_LAUNCH);
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneManager.GetSceneByName("Menu").isLoaded)
        {
            Timer();
            MenuLoad();
        }
        else
        {
            launcherGroup.SetActive(false);
        }

    }

    private void Timer()
    {
        _timer += Time.deltaTime * speed;

        if (_timer >= 1)
        {
            _timer = 1;
        }
    }

    private void MenuLoad()
    {
        loading.fillAmount = _timer;

        if (!isLoading)
        {
            if (loading.fillAmount == 1)
            {
                //AudioManager.instance.Play(AudioManager.AudioList.SFX_LAUNCH);
                SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
                isLoading = true;
            }
        }

    }

}
