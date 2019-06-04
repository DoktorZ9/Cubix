using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Debuger {
    private string Path;
    private void wlog(string text) {
        using (StreamWriter writer = File.AppendText(Path)) {
            try {
                writer.WriteLine(text);
            } catch (Exception ex) {
                Debug.Log(ex.ToString());
            }
            Debug.Log(text);
        }
    }
    public Debuger(string paths) {
        string path = Database.dataPath + "\\" + paths;
        string dir = new Regex(@".[^\\]+$", RegexOptions.Multiline).Replace(path, "");
        try {
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) {
                File.Delete(path);
            }
            Path = path;
        } catch (Exception ex) {
            Debug.Log(ex.ToString());
        }
    }
    public void log(string message) {
        wlog("Info: " + message);
    }

    public void warning(string message) {
        wlog("Warning: " + message);
    }

    public void error(string message) {
        wlog("Error: " + message);
    }
    public void log(string from, string message) {
        wlog("[" + from + "] " + "Info: " + message);
    }

    public void warning(string from, string message) {
        wlog("[" + from + "] " + "Warning: " + message);
    }

    public void error(string from, string message) {
        wlog("[" + from + "] " + "Error: " + message);
    }
}