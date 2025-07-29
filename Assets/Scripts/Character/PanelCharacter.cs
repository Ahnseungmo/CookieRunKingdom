using System.Collections.Generic;
using Spine.Unity;
using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PanelCharacter : Character
{

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CharacterData characterData = new CharacterData();
            characterData.Key = 2;
            CharData = characterData;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CharacterData characterData = new CharacterData();
            characterData.Key = 1;
            CharData = characterData;
        }

    }



}
