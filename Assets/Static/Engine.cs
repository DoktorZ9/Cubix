using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using static Permissions;
public class Engine {
    //Поля
    private Lua _lua;
    private LuaFunction EventManadger;
    private int _id;
    private string _hash;
    private string _namespace;
    private Permissions _permissions;

    public Engine(string path,int id,string nameSpace,int perm) {
        _id = id;
        _hash = Base64Encode(nameSpace);
        _namespace = nameSpace;
        _permissions = new Permissions(perm);
        _lua = new Lua();
        //Дальше идет настройка
        _lua.DoString("getmetatable(\"\").__add = function(x,y) return x..y end");
        _lua.DoString("engineListener = {}; engineListener.callbacks = { }; engineListener.initListener = function(type, table) for i = 1,#engineListener.callbacks do if type == engineListener.callbacks[i].event then engineListener.callbacks[i].callback(table); end end end engineListener.on = function(event,callback) engineListener.callbacks [#engineListener.callbacks+1] = { event = event, callback = callback } return true; end");
        _lua.NewTable("Module");
        LuaTable Module = (LuaTable)_lua["Module"];
            Module["HomeFolder"] = path;
            Module["Id"] = id;
            Module["Namespace"] = nameSpace;
            Module["Hash"] = _hash;
        EventManadger = _lua["engineListener.initListener"] as LuaFunction;
        this.doDir(path);
    }
    public string hash {
        get {
            return _hash;
        }
    }
    public int id {
        get {
            return _id;
        }
    }
    public string nameSpace {
        get {
            return _namespace;
        }
    }
    public Lua engine {
        get {
            return _lua;
        }
    }
    public void doString(string code) {
        _lua.DoString(code);
    }
    public void doFile(string path) {
        using (StreamReader ReadText = new StreamReader(path, System.Text.Encoding.UTF8)) {
            //Парсим, убираем include и прочее...
            string Text = this.sterilization(ReadText.ReadToEnd());
            //Выполняем
            try {
                this.doString(Text);
            } catch (LuaException err) {
                Database.mainDebugger.log(err.Message+"["+err.StackTrace+"]");
            }
        }
        Database.mainDebugger.log("- Def was be loaded: " + path);
    }
    public void doDir(string path) {
        string[] FileNames = Directory.GetFiles(path, "*.lua");
        if (FileNames == null) {
            Database.mainDebugger.log("Didn't find any .lua files");
            return;
        }
        foreach (string s in FileNames) {
            //Ну собсна выполняем файл
            this.doFile(s);
        }
        Database.mainDebugger.log("["+path+"] has been loaded");
    }
    public void sendEvent(string name, string arguments) {
        EventManadger.Call(name, (LuaTable) _lua.DoString("return "+arguments)[0]);
    }
    private string sterilization(string parseValue) {
        //Да-да форич
        //Перебираем совпадения
        foreach (Match match in Regex.Matches(parseValue, @"^@use (.*?);.*$", RegexOptions.Multiline)) {
            //Если нашел совпадения отправляем на обработку
            this.doFile(match.Groups[1].Value);
        }
        foreach (Match match in Regex.Matches(parseValue, @"^@import (.*?);.*$", RegexOptions.Multiline)) {
            //Если нашел совпадения отправляем на обработку
            this.appendModule(_lua,match.Groups[1].Value.ToLower());
        }
        //Удаляем служебные совпадения
        string regex = new Regex(@"^@use (.*?);.*$", RegexOptions.Multiline).Replace(parseValue, "");
        regex = new Regex(@"^@import (.*?);.*$", RegexOptions.Multiline).Replace(regex, "");
        //Удаляем коментарии в виде //
        regex = new Regex(@"\/\/.*$", RegexOptions.Multiline).Replace(regex, "");
        //Возвращаем готовую строку
        return regex;
    }
    private void appendModule(Lua lua,string name) {
        switch (name) {
            case "ui":
                if (_permissions.getPerm("UI_CONTROL")) {
                    _lua["ui"] = new InerfacePrefab(_lua);
                } else {
                    Database.mainDebugger.error("Module Manadger","The namespace ["+this.nameSpace+"] does not have permission to use the component - "+name);
                }
            break;
        }
    }
    private static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}
