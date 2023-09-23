using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Settings
{

	// player settings
	private static Settings instance;
	private GameSettings _settings;
	public GameSettings GameSettings = new GameSettings();

	public bool CanControlPlayer = false;

	private string saveFile = "settings.json";

	public static Settings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Settings();
			}
			return instance;
		}
	}

	public void SaveSettings()
	{
		_settings = new GameSettings
		{
			Sensitivity = GameSettings.Sensitivity,
			Fps = GameSettings.Fps
		};

		string json = JsonUtility.ToJson(_settings, true);

		File.WriteAllText(saveFile, json);
	}

	private void Start()
	{
		Application.targetFrameRate = GameSettings.Fps;
		SaveSettings();
	}
}
