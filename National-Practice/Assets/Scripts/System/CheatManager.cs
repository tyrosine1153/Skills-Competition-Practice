using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheatManager : Singleton<CheatManager>
{
    private readonly Dictionary<KeyCode, Action> _cheatDictionary = new Dictionary<KeyCode, Action>();

    public void AddCheat(KeyCode cheatKeyCode, Action cheatAction)
    {
        print($"Add Cheat: {cheatKeyCode}");
        if (_cheatDictionary.ContainsKey(cheatKeyCode))
        {
            Debug.LogError("This Cheat Key is Already Exist");
            return;
        }

        _cheatDictionary.Add(cheatKeyCode, cheatAction);
    }

    private void Update()
    {
        var actions = _cheatDictionary.Where(x => Input.GetKeyDown(x.Key)).Select(x => x.Value);
        foreach (var action in actions)
        {
            action();
        }
    }
}