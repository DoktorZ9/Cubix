using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permissions{
    private Dictionary<string, int> permlist;
    private Dictionary<string, bool> activitis;
    public Permissions() {
        permlist = new Dictionary<string, int> {
            ["USE_IO"] = 0x1,
            ["USE_MATH"] = 0x2,
            ["USE_TABLE"] = 0x4,
            ["USE_STRING"] = 0x8,
            ["USE_OS"] = 0x10,
            ["UI_CONTROL"] = 0x20
        };
        activitis = new Dictionary<string, bool> {
            ["USE_IO"] = false,
            ["USE_MATH"] = true,
            ["USE_TABLE"] = true,
            ["USE_STRING"] = true,
            ["USE_OS"] = false,
            ["UI_CONTROL"] = false
        };
    }
    public Permissions(int hash):this() {
        this.setHash(hash);
    }
    public int getHash(){
        int resp = 0;
        foreach (string key in activitis.Keys) {
            if (activitis[key]) {
                resp += permlist[key];
            }
        }
        return resp;
    }
    public void setHash(int hash) {
        foreach (string key in permlist.Keys) {
            if ((permlist[key] & hash) != 0) {
                activitis[key] = true;
            }
        }
    }
    public bool getPerm(string key) {
        if (activitis.ContainsKey(key)) {
            return activitis[key];
        } else {
            Database.mainDebugger.log("No such permission exists.");
            return false;
        }
    }
    public void setPerm(string key,bool value) {
        if (activitis.ContainsKey(key)) {
            activitis[key] = value;
        } else {
            Database.mainDebugger.log("No such permission exists.");
        }
    }
}
