using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentSprite{
    private Sprite _sp;
    private List<Action<Sprite>> Callbacks;

    public ComponentSprite() {
        Callbacks = new List<Action<Sprite>>();
    }

    public Sprite GetSprite {
        get {
            return _sp;
        }
    }

    public void onChange(Action<Sprite> f) {
        Callbacks.Add(f);
    }

    private void Change() {
        foreach(Action<Sprite> f in Callbacks) {
            f(_sp);
        }
    }

    public void loadGfx(string filePath) {
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(filePath)) {
            fileData = File.ReadAllBytes(filePath);
            string fileContent = Encoding.ASCII.GetString(fileData);
            if (fileContent.Contains("PNG") || fileContent.Contains("JFIF") || fileContent.Contains("Exif")) {
                tex = new Texture2D(1, 1);
                tex.LoadImage(fileData);
                _sp = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f), 100f);
                this.Change();
            } else {
                Debug.LogError("Unsupported file format");
            }
        } else {
            Debug.LogWarning("Unable to find file at specified path [" + filePath + "]");
        }
    }
}
