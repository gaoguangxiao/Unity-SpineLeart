using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GXButtonScript : MonoBehaviour
{
    public Text TextButton;

    public Text DetailButton;

    public long buttonIndex;

    public void SetText(string text,string detailText)
    {
        TextButton.text = text;

        DetailButton.text = detailText;
    }
}
