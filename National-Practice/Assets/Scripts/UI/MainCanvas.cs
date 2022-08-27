using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainCanvas : MonoBehaviour
    {
        public Button startButton;
        public Button quitButton;
        public Button helpButton;
        
        [Header("Help UI")]
        public Button helpCloseButton;
        public Button helpPrevButton;
        public Button helpNextButton;
        public GameObject[] helpPages;
        private int _helpPageIndex = 0;
        
        private void Start()
        {
            startButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.SaveData(0);
                SceneManagerEx.Instance.LoadScene(SceneType.InGame);
            });
            quitButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
            
            helpButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                _helpPageIndex = 0;
                ShowHelpPage(_helpPageIndex);
            });
            helpCloseButton.onClick.AddListener(() => { AudioManager.Instance.PlaySfx(SfxClip.ButtonClick); });
            helpPrevButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                ShowHelpPage(--_helpPageIndex);
            });
            helpNextButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                ShowHelpPage(++_helpPageIndex);
            });
        }
        
        private void ShowHelpPage(int index)
        {
            helpPrevButton.gameObject.SetActive(index > 0);
            helpNextButton.gameObject.SetActive(index < helpPages.Length - 1);
            foreach (var helpPage in helpPages) helpPage.SetActive(false);
            
            helpPages[index].SetActive(true);
        }
    }
}