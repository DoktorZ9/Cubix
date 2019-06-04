using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ComponentText;

public class RectExt{
    private RectTransform rt;
    
    public RectExt(RectTransform tr) {
        rt = tr;
    }

    public RectTransform getRect() {
        return rt;
    }

    public void setSize(int width,int height) {
        Vector2 deltaSize = new Vector2(width, height) - rt.rect.size;
        rt.offsetMin = rt.offsetMin - new Vector2(deltaSize.x * rt.pivot.x, deltaSize.y * rt.pivot.y);
        rt.offsetMax = rt.offsetMax + new Vector2(deltaSize.x * (1f - rt.pivot.x), deltaSize.y * (1f - rt.pivot.y));
    }
    public void setScale(float scale) {
        rt.localScale = new Vector3(scale, scale, scale);
    }
    public void setFloat(string fl) {
        Vector2 alignMin = new Vector2(0.5f, 0.5f);
        Vector2 alignMax = new Vector2(0.5f, 0.5f);
        switch (fl) {
            case "Full":
                alignMin = new Vector2(0f, 0f);
                alignMax = new Vector2(1f, 1f);
                break;
            case "Center":
                alignMin = new Vector2(0.5f, 0.5f);
                alignMax = new Vector2(0.5f, 0.5f);
                break;
            case "Left":
                alignMin = new Vector2(0f, 0.5f);
                alignMax = new Vector2(0f, 1f);
                break;
            case "Right":
                alignMin = new Vector2(1f, 0.5f);
                alignMax = new Vector2(1f, 0.5f);
                break;
            case "Top":
                alignMin = new Vector2(0.5f, 1f);
                alignMax = new Vector2(0.5f, 1f);
                break;
            case "Bottom":
                alignMin = new Vector2(0.5f, 0f);
                alignMax = new Vector2(0.5f, 0f);
                break;
            case "Right-Bottom":
                alignMin = new Vector2(1f, 0f);
                alignMax = new Vector2(1f, 0f);
                break;
            case "Right-Top":
                alignMin = new Vector2(1f, 1f);
                alignMax = new Vector2(1f, 1f);
                break;
            case "Left-Top":
                alignMin = new Vector2(0f, 1f);
                alignMax = new Vector2(0f, 1f);
                break;
            case "Left-Bottom":
                alignMin = new Vector2(0f, 0f);
                alignMax = new Vector2(0f, 0f);
                break;
        }
        rt.anchorMin = alignMin;
        rt.anchorMax = alignMax;
    }
    public void SetPosition(float x,float y) {
        rt.localPosition = new Vector3(x, y, rt.localPosition.z);
    }
    public void SetRotation(float x,float y,float z) {
        rt.transform.localRotation = Quaternion.Euler(x, y, z);
    }
    public void setParent(ComponentText parent) {
        rt.transform.SetParent(parent.transform.getRect().transform, false);
    }
}
