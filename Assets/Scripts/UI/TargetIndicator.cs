using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    bool _isSlected;
    public bool IsSelected { 
        get {return _isSlected; }
        private set { _isSlected = value; }
    }

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        if (image == null) {
            Debug.LogError("The image component is Missing in " + gameObject.name);
        }
    }

    public void Select()
    {
        IsSelected = true;
        image.enabled = IsSelected;
    }

    public void Deselect() {
        IsSelected = false;
        image.enabled = IsSelected;
    }



}
