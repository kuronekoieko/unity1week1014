using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GetNetaView : MonoBehaviour {

    [SerializeField] Image netaImage;
    public bool isFillGeta { get; private set; }

    // Start is called before the first frame update
    public void OnStart (Vector2 pos) {
        GetComponent<RectTransform> ().anchoredPosition = pos;
    }

    public void Init () {

        netaImage.gameObject.SetActive (false);
        isFillGeta = false;
    }

    public void SetNeta (NetaType netaType) {
        NetaProperty netaProperty = NetaData.i.netaProperties
            .Where (i => i.netaType == netaType)
            .FirstOrDefault ();
        if (netaProperty == null) { return; }

        netaImage.sprite = netaProperty.sprite;
        netaImage.SetNativeSize ();
        netaImage.gameObject.SetActive (true);
        //netaImage.rectTransform.localScale = Vector3.one;

        //netaImage.rectTransform.localScale = new Vector3 (0.4f, 0.3f, 1f);
        isFillGeta = true;
    }

}