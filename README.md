# Myo-Unity Package: EMG Recording
This Unity project contains the required scripts for interfacing the Thalmic Myo armband with Unity, as well as allowing for:
- Live access to the raw EMG data from the Thalmic Myo Armband
- Live filtering of the raw EMG data (moving average filter)
- Easy generation of CSV files containing raw or filtered EMG data and corresponding timestamps

The repository also contains a ready-to-use demo scene.

[![Watch the video](https://github.com/pili-zhangqiu/MyoSaveRoutine/blob/main/img/UnityScene.png)](https://vimeo.com/563161873)


## 📄 Scripts
- ▶️ To see the C# scripts, check the folder: [Assets/Thalmic Myo](https://github.com/pili-zhangqiu/MyoSaveRoutine/tree/main/Assets/Thalmic%20Myo)

## 📂 Unity Folders & Content
The Assets folder contains the Unity project with the demo scenes, assets and C# scripts:
- **Main scene:** /Scenes/SampleMyoSave.unity

#### `Assets/Plugins`
- Contains the plugins required to interface the Thalmic Myo armbands to Unity. In this folder you will find the Dynamic Library Links (.dll) for Microsoft users and the Dynamic Libraries (.dylib) for macOS users.

#### `Assets/Thalmic Myo`
Contains main script for interfacing with the Thalmic Myo (/Myo) and storing EMG data (/MyoEMG).

**/Myo (folder):** Contains the main scripts and prefabs to interface with a Thalmic Myo armband.
>  - **/Myo/Scripts (folder):** Contains the C# scripts to create a ThalmicHub object in Unity and to receive device data / send commands to a Myo armband (e.g. accelerometer, gyroscope, EMG, vibration commands, etc)
>      - **ThalmicHub.cs**: Main file allowing access to one or more Myo bands. For every Myo band connected (up to 2 per Hub), there should be a children ThalmicMyo to a parent ThalmicHub.
>      - **ThalmicMyo.cs**: Children from ThalmicHub. Retrieves IMU and EMG information from the Myo band. New lines were added to the official Thalmic Labs script to share live EMG data.
>  - **/Myo/Scripts/Myo.NET (folder)**: 
>      - **EventTypes.cs**: Defines the Events retrieving the IMU, EMG, etc data from the Myo. A new event argument was added to the official Thalmic Labs script to share live EMG data: *EmgDataEventArgs(Myo myo, DateTime timestamp, int[] emg)* 
>      - **Myo.cs**: Main file defining what is shared by the Thalmic Myo in the Unity interface. New lines were added to the official Thalmic Labs script to share live EMG data.

**/MyoEMG (folder)**:
  Contains real-time EMG moving average filtering script and functions to save EMG data to a CSV file.
>  - **StoreEMG.cs**: Stores real-time EMG data as a list, retrievable by other scripts.
>  - **FunctionsCSV.cs**: contains functions to read/save EMG lists into CSVs. CSV files are store in Assets/MyoEMG/CSV.
>  - **DataFltr.cs**: contains the real-time moving average filter function. Default window size is 5.
>  - **SaveRoutine.cs**: Defines the CSV saving routine (e.g. when to start recording data, when to create the CSV file, etc) and contains the main variable controlling the recording steps: *saveSwitch* (see section *How to Implement the Package*).
>  - **/CSV (folder)**: this folder contains the CSVs for the raw EMG and processed EMG (after using the moving average filter).
>  - **/EMGControllers (folder)**: this folder contains the scripts controlling the EMG data from each of the 8 bipolar sEMG pods in a Thalmic Myo armband. In these scripts, the user can define whether they want to use the raw data or the smoothened one.
  
#### `Scene-focused scripts in Assets`
These are the main scripts controlling the sample scene (*SampleMyoSave.unity*). This scene will allow you to start recording EMG data when the switch button is ON (the button will turn green) and to stop the recording to save the data to a CSV file when the switch button is changed to OFF (the button will turn grey).
  - **ButtonEMG.csv:** Tracks and controls the state of the UI switch button, which allows the player to control the EMG saving routine.
  - **RecordingRoutine.csv:** Defines the different stages of the EMG recording/saving routine. Currently link to the state of the switch button.


## 🧑🏻‍💻 How to Implement the Package
### 1. Setting up your Unity Project:
To add the Myo-Unity EMG recorder capabilities to your Unity project:

<img src="https://github.com/pili-zhangqiu/MyoSaveRoutine/blob/main/img/Myo-Unity_CopyFiles.jpg" width="60%" height="60%">

1. Copy the **Assets/Thalmic Myo folder** to your Unity's project Assets folder.
2. Copy the Thalmic Hub and EMG game element (and children) into your Unity project hierarchy. If required, relink the scripts to the matching C# scripts in the Thalmic Myo folder.
3. To control the EMG recording/saving routine, you can access and change the **saveSwitch** variable value from the */ThalmicMyo/MyoEMG/SaveRoutine.cs* file in the following way:
    - **To start the recording**:
      - **saveSwitch = 0**: Idle *or* fill data holders.
      - **saveSwitch = 1**: Flag to start recording data. Resets the data holders prior to the start of the recording process and defines the start time.
      - **saveSwitch = 0**: Idle *or* fill data holders. It is necessary to change the value to 0 after 1; if not, it will enter a data resetting loop.  
    - **To end the recording and save the CSV data**:
      - **saveSwitch = 2**: Flag to end recording. Saves the 8 sEMG data and timestamps into a uniquely named CSV file (RawEMG-*current_timestamp*) to prevent accidental overwriting. You can find an example CSV file in the project /MyoEMG/CSV folder.
    - You can find an **example of implementing it in your project scripts** in the scripts *Assets/ButtonEMG.csv* and *Assets/RecordingRoutine.csv*.

<img src="https://github.com/pili-zhangqiu/MyoSaveRoutine/blob/main/img/Myo-Unity_StartEndRoutine.jpg" width="75%" height="75%">

### 2. Getting your Myo ready:
For the implementation to work, you will need to have your dongle connected to the PC, as well as your **Myo Connect** application running and your armband connected to *Myo Connect > Armband Manager*. 

<img src="https://user-images.githubusercontent.com/32870045/122043736-982fe000-cdd3-11eb-8f50-907986e53f87.png" width="40%" height="40%">

You will also have to wait for your armband to entered the 'Locked' status, which usually takes a few seconds to warm up. If this does not happen, close the *Myo Connect* application and rerun it.

<img src="https://github.com/pili-zhangqiu/MyoSaveRoutine/blob/main/img/MyoLocked.PNG" width="20%" height="20%">

## 🔎 Troubleshooting:
- **If Unity cannot find the Myo:**
    - Close the *Myo Connect* application and rerun it.
    - Make sure that the dongle is connected and the device appears in *Myo Connect > Armband Manager*
    - Open http://diagnostics.myo.com/. If the Bluetooth communication between Myo and PC is working, you will be able to access its data through the webapp
- **If you get a Unity error saying *ThalmicHub couldn't be initialized***:
    - Extra Unity Free Steps:
        - If you have Unity Pro, you’re all set up. If you are using the **free Unity version**, do the following: 
        - Take the myo.dll that’s in the plugins/x86 folder (if you are using Windows OS) and drop it into your main project folder. Follow these steps:
    - Further information can be found at: https://developerblog.myo.com/setting-myo-package-unity/



