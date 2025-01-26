using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollerCoasterImage : MonoBehaviour
{
    private RawImage image;

    void Start()
    {
        if (AllManager.Instance.rollerCoasterImage != null)
        { 
        image = GetComponent<RawImage>();

        image.texture = AllManager.Instance.rollerCoasterImage;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
