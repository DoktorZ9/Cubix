using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ComponentSprite;
using static RectExt;

public class ComponentImage{
    private Image _target;
    private GameObject gameObject;
    private RectExt RextRrt;

    public ComponentImage(Image _,GameObject __) {
        _target = _;
        gameObject = __;
        RextRrt = new RectExt(__.GetComponent<RectTransform>());
    }

    public void setSprite(ComponentSprite sprite) {
        _target.sprite = sprite.GetSprite;
        sprite.onChange(delegate (Sprite s){
            _target.sprite = s;
        });
    }

    public RectExt transform {
        get {
            return RextRrt;
        }
    }
}
