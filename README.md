# Myo-Unity Package: EMG Recording
This Unity project contains the required scripts for interfacing the Thalmic Myo armband with Unity, as well as allowing for:
- Live access to the raw EMG data from the Thalmic Myo Armband
- Live filtering of the raw EMG data (moving average filter)
- Easy generation of CSV files containing raw or filtered EMG data and corresponding timestamps

The repository also contains a ready-to-use demo scene.

[![Watch the video](https://github.com/pili-zhangqiu/MyoSaveRoutine/blob/main/img/UnityScene.png)](https://vimeo.com/563161873)

## 01 - Essential folders in Assets
The Assets folder contains the Unity project with the demo scenes, assets and C# scripts:

**Main scene:** /Scenes/SampleMyoSave.unity

### 01.A - Assets/Plugins:
Contains the plugins required to interface the Thalmic Myo armbands to Unity. In this folder you will find the Dynamic Library Links (.dll) for Microsoft users and the Dynamic Libraries (.dylib) for macOS users.

### 01.B - Assets/Thalmic Myo:
Contains main script for interfacing with the Thalmic Myo (/Myo) and storing EMG data (/MyoEMG).
- **/Myo (folder):** Contains the main scripts and prefabs to interface with a Thalmic Myo armband.
  - **/Myo/Scripts (folder):** Contains the C# scripts to create a ThalmicHub object in Unity and to receive device data / send commands to a Myo armband (e.g. accelerometer, gyroscope, EMG, vibration commands, etc)
    - **ThalmicHub.cs**: Main file allowing access to one or more Myo bands. For every Myo band connected (up to 2 per Hub), there should be a children ThalmicMyo to a parent ThalmicHub.
    - **ThalmicMyo.cs**: Children from ThalmicHub. Retrieves IMU and EMG information from the Myo band. New lines were added to the official Thalmic Labs script to share live EMG data.
  - **/Myo/Scripts/Myo.NET (folder)**: 
    - **EventTypes.cs**: Defines the Events retrieving the IMU, EMG, etc data from the Myo. A new event argument was added to the official Thalmic Labs script to share live EMG data: *EmgDataEventArgs(Myo myo, DateTime timestamp, int[] emg)* 
    - **Myo.cs**: Main file defining what is shared by the Thalmic Myo in the Unity interface. New lines were added to the official Thalmic Labs script to share live EMG data.

- **/MyoEMG (folder)**:
  Contains real-time EMG moving average filtering script and functions to save EMG data to a CSV file.
  - **StoreEMG.cs**: Stores real-time EMG data as a list, retrievable by other scripts.
  - **FunctionsCSV.cs**: contains functions to read/save EMG lists into CSVs. CSV files are store in Assets/MyoEMG/CSV.
  - **DataFltr.cs**: contains the real-time moving average filter function. Default window size is 5.
  - **SaveRoutine.cs**: Defines the CSV saving routine (e.g. when to start recording data, when to create the CSV file, etc) and contains the main variable controlling the recording steps: *saveSwitch* (see section *02 - How to Implement the Package*).
  - **/CSV (folder)**: this folder contains the CSVs for the raw EMG and processed EMG (after using the moving average filter).
  - **/EMGControllers (folder)**: this folder contains the scripts controlling the EMG data from each of the 8 bipolar sEMG pods in a Thalmic Myo armband. In these scripts, the user can define whether they want to use the raw data or the smoothened one.

### 01.B - Assets/Thalmic Myo:
To replicate the Myo-Unity implementation, as well as the data recording capabilities, copy the following folder to your project. 
  
### 01.C - Scene-focused scripts in Assets:
These are the main scripts controlling the sample scene (*SampleMyoSave.unity*). This scene will allow you to start recording EMG data when the switch button is ON (the button will turn green) and to stop the recording to save the data to a CSV file when the switch button is changed to OFF (the button will turn grey).
- **ButtonEMG.csv:** Tracks and controls the state of the UI switch button, which allows the player to control the EMG saving routine.
- **RecordingRoutine.csv:** Defines the different stages of the EMG recording/saving routine. Currently link to the state of the switch button.



