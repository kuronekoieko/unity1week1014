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
        netaImage.gameObject.SetActive (false);
        GetComponent<RectTransform> ().anchoredPosition = pos;
    }

    public void SetNeta (NetaType netaType) {
        NetaProperty netaProperty = NetaData.i.netaProperties
            .Where (i => i.netaType == netaType)
            .FirstOrDefault ();
        if (netaProperty == null) { return; }

        netaImage.gameObject.SetActive (true);
        netaImage.sprite = netaProperty.sprite;
        isFillGeta = true;
    }

}