using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] GetNetaView getNetaViewPrefab;
    [SerializeField] RectTransform getNetaViewParent;
    [SerializeField] Text resultText;
    [SerializeField] Text stageNumText;
    [SerializeField] Button retryButton;
    [SerializeField] Button tweetButton;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image clearImage;

    GetNetaView[] netaViews;
    Vector2 offset = new Vector2 (130f, 0f);
    Vector2 startPos = new Vector2 (-350f, -180f);

    public static UIManager i;

    // Start is called before the first frame update
    public void OnStart () {
        NetaViewGenerator ();
        i = this;
        retryButton.onClick.AddListener (OnClickRetryButton);
        tweetButton.onClick.AddListener (OnClickTweetButton);

    }

    public void Init () {
        for (int i = 0; i < netaViews.Length; i++) {
            netaViews[i].Init ();
        }
        resultText.gameObject.SetActive (false);
        backgroundImage.gameObject.SetActive (false);
        clearImage.gameObject.SetActive (false);
        stageNumText.text = "ステージ" + (Variables.stageIndex + 1);
        SetActiveButtons (isActive: false);
    }

    void NetaViewGenerator () {
        netaViews = new GetNetaView[3];

        Vector2 pos = startPos;

        for (int i = 0; i < netaViews.Length; i++) {
            netaViews[i] = Instantiate (
                getNetaViewPrefab,
                Vector3.zero,
                Quaternion.identity,
                getNetaViewParent);
            netaViews[i].OnStart (pos);
            pos += offset;
        }
    }

    public void GetNeta (NetaType netaType) {
        for (int i = 0; i < netaViews.Length; i++) {

            if (!netaViews[i].isFillGeta) {
                netaViews[i].SetNeta (netaType);
                return;
            }
        }
    }

    public void ShowResultText (string result) {
        backgroundImage.gameObject.SetActive (true);
        clearImage.gameObject.SetActive (true);
        ShowAnim ();
        //resultText.gameObject.SetActive (true);
        resultText.text = result;
    }

    void OnClickRetryButton () {
        AudioManager.i.PlayOneShot (1);
        Variables.gameState = GameState.START;
        Variables.stageIndex = 0;
       // Variables.speed = 0.6f;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync ("Ranking");
    }
    void OnClickTweetButton () {
        AudioManager.i.PlayOneShot (1);
        string tweetText = "あなたのスコアは…\n\n" +
            "ステージ：" + (Variables.stageIndex + 1) +
            "\n\nでした！！みんなもやってみよう！！" +
            "\n\n#sushisagashi\n#unity1week\n";
        try {
            naichilab.UnityRoomTweet.Tweet ("sushisagashi", tweetText);
        } catch (System.Exception) {

            throw;
        }
    }

    public void SetActiveButtons (bool isActive) {
        retryButton.gameObject.SetActive (isActive);
        tweetButton.gameObject.SetActive (isActive);
    }

    void ShowAnim () {
        float duration = 0.5f;
        clearImage.rectTransform.anchoredPosition = new Vector2 (0, 470);
        clearImage.rectTransform
            .DOLocalMoveY (0, duration)
            .SetEase (Ease.OutBack)
            .OnComplete (() => {
                AudioManager.i.PlayOneShot (2);
                DOVirtual.DelayedCall (0.5f, () => HideAnim ());
            });
        //backgroundImage.color = new Color (0, 0, 0, 0);
        backgroundImage.CrossFadeAlpha (0.5f, duration, true);
    }

    void HideAnim () {
        float duration = 0.5f;
        clearImage.rectTransform
            .DOLocalMoveY (-470, duration)
            .SetEase (Ease.InBack)
            .OnComplete (() => {
                OnHideAnimEnd ();
            });
        //backgroundImage.color = new Color (0, 0, 0, 0);
        backgroundImage.CrossFadeAlpha (0f, duration, true);
    }

    void OnHideAnimEnd () {
        if (StageData.i.list.Count == Variables.stageIndex + 1) {
            Debug.Log ("全クリ");
            Variables.gameState = GameState.RESULT;
        } else {
            Variables.gameState = GameState.START;
            Variables.stageIndex++;
            //SetSpeed ();
        }
    }
    /*void SetSpeed () {
            int stageNum = Variables.stageIndex + 1;
            if (IsChangeSpeed (stageNum)) {
                Variables.speed -= 0.1f;
            }
            Variables.speed = Mathf.Clamp (Variables.speed, 0.1f, 0.6f);
            Debug.Log (stageNum + "  " + Variables.speed);
        } */

    bool IsChangeSpeed (int stageNum) {
        if (stageNum <= 5) {
            if (stageNum % 3 == 1) {
                return true;
            }
        } else {
            if (stageNum % 2 == 1) {
                return true;
            }
        }
        return false;
    }

}