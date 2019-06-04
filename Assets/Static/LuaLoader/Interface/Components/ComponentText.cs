using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RectExt;

public class ComponentText {
    private RectTransform tran;
    private Text textComponent;
    private GameObject targetObject;
    private string layerName;
    private RectExt RextRrt;

    public ComponentText(GameObject _Object,Text txt,string _ln){
        targetObject = _Object;
        textComponent = txt;
        tran = _Object.GetComponent<RectTransform>();
        layerName = _ln;
        RextRrt = new RectExt(tran);
        this.setFont("Default");
    }

    public RectExt transform {
        get {
            return RextRrt;
        }
    }

    public void setAligment(string aligment) {
        if (aligment.Trim() == "") {
            textComponent.alignment = TextAnchor.MiddleCenter;
        } else {
            switch (aligment) {
                case "UpperLeft":
                    textComponent.alignment = TextAnchor.UpperLeft;
                    break;
                case "UpperCenter":
                    textComponent.alignment = TextAnchor.UpperCenter;
                    break;
                case "UpperRight":
                    textComponent.alignment = TextAnchor.UpperRight;
                    break;
                case "MiddleLeft":
                    textComponent.alignment = TextAnchor.MiddleLeft;
                    break;
                case "MiddleCenter":
                    textComponent.alignment = TextAnchor.MiddleCenter;
                    break;
                case "MiddleRight":
                    textComponent.alignment = TextAnchor.MiddleRight;
                    break;
                case "LowerLeft":
                    textComponent.alignment = TextAnchor.LowerLeft;
                    break;
                case "LowerCenter":
                    textComponent.alignment = TextAnchor.LowerCenter;
                    break;
                case "LowerRight":
                    textComponent.alignment = TextAnchor.LowerRight;
                    break;
            }
        }
    }

    public void setColor(string color) {
        if (color.Length < 6 && color.Length > 7) {
            Database.mainDebugger.error("Text.setColor", "The minimum long hex string is 6, and the maximum is 7");
            return;
        }
        if (color[0] == '#')
            color = color.Substring(1);
        string r = "" + color[0] + color[1];
        string g = "" + color[2] + color[3];
        string b = "" + color[4] + color[5];

        textComponent.color = new UnityEngine.Color(byte.Parse(r), byte.Parse(g), byte.Parse(b), 1);
    }
    public void setColor(byte r,byte g,byte b) {
        textComponent.color = new UnityEngine.Color(r,g,b,1);
    }

    public void setFontSize(int size) {
        textComponent.fontSize = size;
    }

    public void setText(string text) {
        textComponent.text = text;
    }

    public void setFont(string _font) {
        switch (_font) {
            case "Default":
                textComponent.font = Resources.Load<Font>("Fonts/16908");
                break;
            default:
                textComponent.font = Resources.Load<Font>("Fonts/16908");
                break;
        }
    }
}
