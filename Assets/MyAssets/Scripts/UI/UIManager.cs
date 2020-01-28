using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GetNetaView getNetaViewPrefab;
    [SerializeField] RectTransform getNetaViewParent;
    [SerializeField] Text stageNumText;
    [SerializeField] Button retryButton;
    [SerializeField] Button tweetButton;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image clearImage;

    GetNetaView[] netaViews;
    Vector2 offset = new Vector2(200f, 0f);
    Vector2 startPos = new Vector2(-550f, -280f);

    public static UIManager i;

    // Start is called before the first frame update
    public void OnStart()
    {
        NetaViewGenerator();
        i = this;
        retryButton.onClick.AddListener(OnClickRetryButton);
        tweetButton.onClick.AddListener(OnClickTweetButton);

    }

    public void Init()
    {
        for (int i = 0; i < netaViews.Length; i++)
        {
            netaViews[i].Init();
        }
        backgroundImage.gameObject.SetActive(false);
        clearImage.gameObject.SetActive(false);
        stageNumText.text = "ステージ" + (Variables.stageIndex + 1);
        SetActiveButtons(isActive: false);
    }

    void NetaViewGenerator()
    {
        netaViews = new GetNetaView[3];

        Vector2 pos = startPos;

        for (int i = 0; i < netaViews.Length; i++)
        {
            netaViews[i] = Instantiate(
                getNetaViewPrefab,
                Vector3.zero,
                Quaternion.identity,
                getNetaViewParent);
            netaViews[i].OnStart(pos);
            pos += offset;
        }
    }

    public void GetNeta(NetaType netaType)
    {

        for (int i = 0; i < netaViews.Length; i++)
        {
            if (!netaViews[i].isFillGeta)
            {
                netaViews[i].SetNeta(netaType);
                return;
            }
        }
    }

    public void ShowClearImage()
    {
        backgroundImage.gameObject.SetActive(true);
        clearImage.gameObject.SetActive(true);
        ShowAnim();
    }

    void OnClickRetryButton()
    {
        AudioManager.i.PlayOneShot(1);
        //リセットするとやる気をなくすのでやらない
        //AudioManager.i.RePlayBGM ();
        Variables.gameState = GameState.START;
        Variables.stageIndex = 0;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ranking");
    }
    void OnClickTweetButton()
    {
        AudioManager.i.PlayOneShot(1);
        string tweetText = "あなたのスコアは…\n\n" +
            "ステージ：" + (Variables.stageIndex + 1) +
            "\n\nでした！！みんなもやってみよう！！" +
            "\n\n#sushisagashi\n#unity1week\n";
        try
        {
            //naichilab.UnityRoomTweet.Tweet("sushisagashi", tweetText);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void SetActiveButtons(bool isActive)
    {
        retryButton.gameObject.SetActive(isActive);
        tweetButton.gameObject.SetActive(isActive);
    }

    void ShowAnim()
    {
        float duration = 0.5f;
        clearImage.rectTransform.anchoredPosition = new Vector2(0, 470);
        clearImage.rectTransform
            .DOLocalMoveY(0, duration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                AudioManager.i.PlayOneShot(2);
                DOVirtual.DelayedCall(0.5f, () => HideAnim());
            });

        //背景画像を暗くするアニメーション
        DOTween.ToAlpha(
                () => backgroundImage.color,
                color => backgroundImage.color = color,
                endValue: 0.3f,
                duration: duration)
            .SetEase(Ease.Linear);
    }

    void HideAnim()
    {
        float duration = 0.5f;
        clearImage.rectTransform
            .DOLocalMoveY(-470, duration)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                OnHideAnimEnd();
            });

        //背景画像を暗くするアニメーション
        DOTween.ToAlpha(
                () => backgroundImage.color,
                color => backgroundImage.color = color,
                endValue: 0f,
                duration: duration)
            .SetEase(Ease.Linear);
    }

    void OnHideAnimEnd()
    {

        if (StageData.i.list.Count == Variables.stageIndex + 1)
        {
            //全クリ
            Variables.gameState = GameState.RESULT;
        }
        else
        {
            //通常クリア
            Variables.gameState = GameState.START;
            //Variables.stageIndex = 18;
        }
        Variables.stageIndex++;
    }

}