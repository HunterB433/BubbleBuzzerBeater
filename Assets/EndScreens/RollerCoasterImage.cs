using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollerCoasterImage : MonoBehaviour
{
    private RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();

        image.texture = AllManager.Instance.rollerCoasterImage;
    }
}
