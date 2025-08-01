using System.Collections.Generic;
using Spine.Unity;
using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PanelCharacter : Character
{
    private int _key;
    
    public int Key
    {
        get { return _key; }
        set
        {
            _key = value;
            CharacterData characterData = DataManager.Instance.GetCharacterData(_key);
            CharData = characterData;
            _state = new CharState();
            _state = CharState.Idle;
        }
    }

}
