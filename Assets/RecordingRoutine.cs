using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RecordingRoutine : MonoBehaviour 
{
    private int secondsStart;
    private int secondsDelta;

    private bool startCounter = false;   // flag to start the ountdown timer (true)
    private bool ongoingCounter = false; // status of the counter: counting down (true) or stopped (false)

    private bool currentButtonEmgOnState = false;
    private bool lastButtonEmgOnState = false;


    void Update()
    {
        // =========================== Button Routine Starter ===========================
        currentButtonEmgOnState = ButtonEMG.buttonEmgOnState;   // Check the state of the EMG recording button

        // If the button state just changed to recording ON, then set the startCounter value to true
        if (currentButtonEmgOnState == true & lastButtonEmgOnState != currentButtonEmgOnState)
        {
            startCounter = true;

            // Reset the data holders before starting to record
            GameObject saveObject = UnityEngine.GameObject.Find("EMG/SaveController");
            SaveRoutine saveScript = saveObject.GetComponent<SaveRoutine>();

            saveScript.saveSwitch = 1;
            saveScript.saveSwitch = 0;  // Prevents it entering a "data deletion" loop

        }

        // If the button state just changed to recording OFF, then set to false
        if (currentButtonEmgOnState == false & lastButtonEmgOnState != currentButtonEmgOnState)
        {
            startCounter = false;
            ongoingCounter = false;

            // Change the text GameObject
            TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
            textmeshPro.SetText("Saving data. This can take a few minutes...");

            // Save data into a CSV file by changing the value of the saveSwitch
            GameObject saveObject = UnityEngine.GameObject.Find("EMG/SaveController");
            SaveRoutine saveScript = saveObject.GetComponent<SaveRoutine>();

            saveScript.saveSwitch = 2;

        }

        // =========================== Dynamic Recording State ===========================
        // Only called once when a timer starts
        if (startCounter == true)
        {
            secondsStart = DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second;
            startCounter = false;
            ongoingCounter = true;
        }

        // Called when a timer is running
        if (ongoingCounter == true)
        {
            // Calculate the duration of the current recording
            secondsDelta = DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second - secondsStart;

            // Change the text GameObject
            TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
            textmeshPro.SetText("Recording for: " + secondsDelta.ToString() + " secs");
        }
        else
        {
            // If the timer hasn't been started, give the player information on how to start the timer to record the Myo band EMG
            TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
            textmeshPro.SetText("Not recording");
        }

        // ===========================Save the state to compare in the next frame ===========================
        lastButtonEmgOnState = currentButtonEmgOnState;
    }
}