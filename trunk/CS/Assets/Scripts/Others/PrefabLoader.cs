using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * chua lien ket toi nhung prefab can thiet trong game
 * tat ca cac prefab se dc load ngay khi game dc khoi tao 
 * 
*/




public class PrefabLoader : MonoBehaviour {

    public static List<CSMapPrefab> maps = new List<CSMapPrefab>();
    public static List<CSWeaponPrefab> weapons = new List<CSWeaponPrefab>();
    public static List<CSCharacterPrefab> characters = new List<CSCharacterPrefab>();
    

    protected static void LoadMaps()
    {
        UnityEngine.Object[] _availableMaps = Resources.LoadAll(@"Maps");
        CSMapPrefab map;
        foreach (UnityEngine.Object t in _availableMaps)
        {
            map = new CSMapPrefab();
            map._model = (GameObject)t;
            map._name = ((GameObject)t).name;
            maps.Add(map);
        }        
    }

    protected static void LoadCharacters()
    {
        UnityEngine.Object[] _availableCharacters = Resources.LoadAll(@"Characters");
        CSCharacterPrefab cha;
        foreach (UnityEngine.Object t in _availableCharacters)
        {
            cha = new CSCharacterPrefab();
            cha._model = (GameObject)t;
            cha._name = ((GameObject)t).name;
            characters.Add(cha);
        }
    }

    protected static void LoadWeapons()
    {
        UnityEngine.Object[] _availableWeapons = Resources.LoadAll(@"Weapons");
        CSWeaponPrefab wea;
        foreach (UnityEngine.Object t in _availableWeapons)
        {
            wea = new CSWeaponPrefab();
            wea._model = (GameObject)t;
            wea._name = ((GameObject)t).name;
            weapons.Add(wea);
        }
    }


    public static GameObject GetMapModel(string name)
    {
        foreach(CSMapPrefab p in maps)
        {
            if(p._name == name)
                return p._model;
        }
        return null;
    }

    public static GameObject GetCharacterModel(string name)
    {
        foreach (CSCharacterPrefab p in characters)
        {
            if (p._name == name)
                return p._model;
        }
        return null;
    }

    public static GameObject GetWeaponModel(string name)
    {
        foreach (CSWeaponPrefab p in weapons)
        {
            if (p._name == name)
                return p._model;
        }
        return null;
    }


    // load at game stating
    void Start()
    {
        LoadMaps();
        LoadWeapons();
        LoadCharacters();
    }
}
