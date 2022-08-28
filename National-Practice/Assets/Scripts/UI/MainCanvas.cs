using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainCanvas : MonoBehaviour
    {
        public Button newGameButton;
        public Button continueButton;
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
            continueButton.interactable = GameManager.Instance.LoadData() > 0;

            newGameButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.SaveData(0);
                SceneManagerEx.Instance.LoadScene(SceneType.InGame);
            });
            continueButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.LoadData();
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
            for (int i = 0; i < helpPages.Length; i++)
            {
                helpPages[i].SetActive(i == index);
            }
        }
    }
}