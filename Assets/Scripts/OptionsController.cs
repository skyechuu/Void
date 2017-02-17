using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class GameOptions{
    public List<Constant> constants;

    public GameOptions(){
        constants = new List<Constant>();
    }
}

[System.Serializable]
public class Constant{

    static int count=0;
    public int id;
    public string name;
    public string val;

    public Constant(string n, string v){
        name=n;
        val=v;
        id=count;
        count++;
    }
}

[System.Serializable]
public class OptionsController : MonoBehaviour {
    public static GameOptions opt;
    public Texture2D defaultCursor;

    void Awake(){
        opt = new GameOptions();
        LoadGameOptions();
        Debug.Log("Welcome "+GetConstantValue(0)+"!");
        Cursor.SetCursor(defaultCursor,new Vector2(16,16),CursorMode.ForceSoftware);
    }

    public static void AddConstant(string n, string v){
        foreach(Constant c in opt.constants){
            if(c.name == n){
                ChangeConstant(n,v);
            }
        }
        opt.constants.Add(new Constant(n,v));
    }

    public static Constant FindConstant(string n){
        foreach(Constant c in opt.constants){
            if(c.name == n){
                return c;
            }
        }
        return null;
    }

    public static Constant FindConstant(int i){
        foreach(Constant c in opt.constants){
            if(c.id == i){
                return c;
            }
        }
        return null;
    }
    
    public static string GetConstantValue(int i){
        if(FindConstant(i) != null)
            return FindConstant(i).val;
        return null;

    }

    public static string GetConstantValue(string n){
        if(FindConstant(n) != null)
            return FindConstant(n).val;
        return null;
    }

    public static void ChangeConstant(string n, string v){
        foreach(Constant c in opt.constants){
            if(c.name == n){
                c.val = v;
            }
        }
    }

    public static void ChangeConstant(int i, string v){
        foreach(Constant c in opt.constants){
            if(c.id == i){
                c.val = v;
            }
        }
    }

    public static void DeleteConstant(string n){
        opt.constants.Remove(FindConstant(n));
    }

    public static void DeleteConstant(int i){
        opt.constants.Remove(FindConstant(i));
    }

    public static void SaveGameOptions(){
        if(opt.constants.Count>0){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create (Application.persistentDataPath + "/"+FindConstant(0).val+"_options.dat");
            bf.Serialize(file, opt);
            file.Close();
            PlayerPrefs.SetString("LastProfile",GetConstantValue(0));
        }
        else{
            opt.constants.Add(new Constant("profile","default"));
            SaveGameOptions();
        }
        
    }

    public static void LoadGameOptions(){
        if(File.Exists(Application.persistentDataPath + "/"+PlayerPrefs.GetString("LastProfile","default")+"_options.dat")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open (Application.persistentDataPath + "/"+PlayerPrefs.GetString("LastProfile","default")+"_options.dat",FileMode.Open);
            opt = (GameOptions) bf.Deserialize(file);
            file.Close();
        }
        else{
            CreateFirstOptionsFile();
        }
    }

    static void CreateFirstOptionsFile(){
        SaveGameOptions();
        LoadGameOptions();
    }
	
}
