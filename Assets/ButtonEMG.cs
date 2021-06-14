using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ButtonEMG : MonoBehaviour
{
	public static bool buttonEmgOnState = false;    // Start with button OFF

	void Start()
	{
		// Start button click listener
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void Update()
    {
		// If state is false, set colour to red
		if (buttonEmgOnState == false)
        {
			Color greyOff = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1f);
			ChangeButtonColour(greyOff);
		}

		// If state is true, set colour to green
		else
		{
			Color greenOn = new Color(136 / 255f, 221 / 255f, 78 / 255f, 1f);
			ChangeButtonColour(greenOn);
		}
	}


	// ====================== This function is only called when the button is clicked ======================
	void TaskOnClick()
	{
		UnityEngine.Debug.Log("You have clicked the button!");
		buttonEmgOnState = !buttonEmgOnState;	// Toggle the state of the button everytime it is clicked
	}


	// ============================== Function to change button selected colour ============================
	void ChangeButtonColour(Color newColor)
    {
		// Change the color of the button
		Button btn = GetComponent<Button>();
		ColorBlock btnColor = btn.colors;
		btnColor.selectedColor = newColor;
		btn.colors = btnColor;
	}
}