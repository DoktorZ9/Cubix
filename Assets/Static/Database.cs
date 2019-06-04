using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Debuger;
public static class Database{
    //Путь к рабочей папке
    public static string dataPath = Application.isEditor ? Environment.CurrentDirectory + "\\Assets\\" : Environment.CurrentDirectory;
    public static Debuger mainDebugger = new Debuger("logs\\mainDebugger.txt");
}
