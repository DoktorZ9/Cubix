using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interface;
using LuaInterface;

public class InerfacePrefab {
    Dictionary<string, Interface> Interfaces = new Dictionary<string, Interface>();
    Lua Engine;

    public InerfacePrefab(Lua lua) {
        Engine = lua;
    }

    public Interface createInterface(string name) {
        Interface target = new Interface(Engine, name);
        Interfaces.Add(name, target);
        return target;
    }
}
