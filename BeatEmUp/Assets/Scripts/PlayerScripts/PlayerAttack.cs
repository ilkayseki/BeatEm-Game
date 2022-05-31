using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}

public class PlayerAttack : MonoBehaviour
{
    private CharacterAnimation playerAnim; //animasyonları ayarlamak için

    private bool activateTimerToReset; //zamanlayıcıyı sıfırlamak için etkinleştir

    private float defaultComboTimer = 0.4f; //varsayılan karışım zamanlayıcı
    private float currentComboTimer; // mevcut karışım zamanlayıcı

    private ComboState currentComboState; //mevcut karışım(combo) durumu

    // Start is called before the first frame update
    void Awake()
    {
        playerAnim = GetComponentInChildren<CharacterAnimation>();
    }

    private void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }

    //combo attack
    private void ComboAttacks()
    {
        //if punch
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentComboState == ComboState.PUNCH_3 ||
               currentComboState == ComboState.KICK_1 ||
               currentComboState == ComboState.KICK_2)
                return;
            //void fonk da geri döndürecek bir şey yoktur o yüzden buradaki return fonksiyonun dışına atar bizi.

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if(currentComboState == ComboState.PUNCH_1)
            {
                playerAnim.Punch1();
            }
            if (currentComboState == ComboState.PUNCH_2)
            {
                playerAnim.Punch2();
            }
            if (currentComboState == ComboState.PUNCH_3)
            {
                playerAnim.Punch3();
            }
        }//if punch

        //if kick
        if (Input.GetKeyDown(KeyCode.X))
        {
            //if the current combo is punch3 or kick 2 
            //return meaning exit because we have no combos to perform
            if (currentComboState == ComboState.KICK_2 ||
               currentComboState == ComboState.PUNCH_3)
                return;
            //if the current combo state is NONE, or punch 1 or punch2
            //then we can set current combo state to kick1 to chain the combo
            if(currentComboState == ComboState.NONE ||
               currentComboState == ComboState.PUNCH_1 ||
               currentComboState == ComboState.PUNCH_2)
            {
                currentComboState = ComboState.KICK_1;
            } else if(currentComboState == ComboState.KICK_1)
            {   //move to kick2
                currentComboState++;
            }

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if(currentComboState == ComboState.KICK_1)
            {
                playerAnim.Kick1();
            }
            if(currentComboState == ComboState.KICK_2)
            {
                playerAnim.Kick2();
            }
        }//if kick

    }//combo attack

    //reset combo state
    private void ResetComboState()
    {
        if (activateTimerToReset) //"zamanlayıcıyı sıfırlamak için etkinleştir" true ise
        {
            currentComboTimer -= Time.deltaTime;

            if(currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;

                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }//reset combo state
}
