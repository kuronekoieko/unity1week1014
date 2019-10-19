using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] GetNetaView getNetaViewPrefab;
    [SerializeField] RectTransform getNetaViewParent;
    [SerializeField] Text resultText;
    [SerializeField] Text stageNumText;
    [SerializeField] Button retryButton;
    [SerializeField] Button tweetButton;

    GetNetaView[] netaViews;
    Vector2 offset = new Vector2 (100f, 0f);
    Vector2 startPos = new Vector2 (-300f, -180f);

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
        resultText.gameObject.SetActive (true);
        resultText.text = result;
    }

    void OnClickRetryButton () {
        Variables.gameState = GameState.START;
        Variables.stageIndex = 0;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync ("Ranking");
    }
    void OnClickTweetButton () {
        //AudioManager.i.PlayOneShot (0);
        string tweetText = "あなたのスコアは…\n\n" +
            "ステージ：" + (Variables.stageIndex + 1) +
            "\n\nでした！！みんなもやってみよう！！" +
            "\n\n#Osushi\n#unity1week\n";
        try {
            naichilab.UnityRoomTweet.Tweet ("", tweetText);
        } catch (System.Exception) {

            throw;
        }
    }

    public void SetActiveButtons (bool isActive) {
        retryButton.gameObject.SetActive (isActive);
        tweetButton.gameObject.SetActive (isActive);
    }

}