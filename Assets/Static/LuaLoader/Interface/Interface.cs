using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LuaInterface;
using static ComponentText;
using static ComponentSprite;
using static ComponentImage;

public class Interface{
    private Lua Engine;
    private Canvas canvas;
    private GameObject canvasObject;
    private int HasEl = 0;
    Dictionary<string, Sprite> sprites;

    public Interface(Lua _lua,string name) {
        Engine = _lua;
        canvasObject = new GameObject("CANVAS #"+name);
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();
        canvasObject.SetActive(false);
        sprites = new Dictionary<string, Sprite>();
    }

    public object createComponent(string tag) {
        HasEl++;
        string name = getRandomName(HasEl, tag);
        GameObject _object;
        switch (tag) {
            case "text":
                _object = new GameObject(name);
                Text textComponent = _object.AddComponent<Text>();
                _object.transform.SetParent(canvasObject.transform, false);
                return (ComponentText) new ComponentText(_object,textComponent, name);
            case "sprite":
                return (ComponentSprite) new ComponentSprite();
            case "img":
                _object = new GameObject(name);
                Image _ImageObj = _object.AddComponent<Image>();
                _object.transform.SetParent(canvasObject.transform, false);
                return (ComponentImage) new ComponentImage(_ImageObj,_object);
        }
        return null;
    }

    private string getRandomName(int index,string tag) {
        return "[" + tag + "] - " + index;
    }
}
