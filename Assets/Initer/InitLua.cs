using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;
using static Engine;

public class InitLua : MonoBehaviour{
    private List<Engine> Engines = new List<Engine>();
    void Awake(){
        int start = Environment.TickCount & Int32.MaxValue;
        initNew(Database.dataPath + "Defs","System", 0x3F);
        int end = Environment.TickCount & Int32.MaxValue;
        //Говорим всем скриптам что движок загружен
        //После загрузки должна начатся загрузка ассетов и п.р.
        this.InitListener("load", "{took="+(end-start)+"}");
    }
    public void InitListener(string name, string arguments) {
        foreach(Engine eng in Engines) {
            eng.sendEvent(name, arguments);
        }
    }
    public void InitListener(string name) {
        foreach (Engine eng in Engines) {
            eng.sendEvent(name, "{}");
        }
    }
    private void initNew(string Path,string nameSpace,int permissions){
        Engine _ = new Engine(Path, Engines.Count, nameSpace, permissions);
        Engines.Add(_);
    }
}
