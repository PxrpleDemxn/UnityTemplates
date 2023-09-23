using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
	// world settings
	public float Gravity { get; set; } = -9.81f;
	// speed
	public float Speed { get; set; }
	public float WalkSpeed { get; set; } = 5f;
	public float SprintSpeed { get; set; } = 10f;
	public float CrouchSpeed { get; set; } = 2f;

	/// <summary>
	/// Sets the player's speed.
	/// </summary>
	/// <param name="speed"></param>
	public void SetSpeed(float speed)
	{
		Speed = speed;
	}

	// health
	public float Health { get; set; } = 100f;
	public float MaxHealth { get; set; } = 100f;

	// stamina
	public float Stamina { get; set; } = 100f;
	public float MaxStamina { get; set; } = 100f;

	// level
	public int Level { get; set; } = 1;
	public float Experience { get; set; } = 0f;
	public float ExperienceToNextLevel { get; set; } = 100f;

	/// <summary>
	/// Adds experience to the player and levels up if the experience is greater than the experience needed to level up.
	/// </summary>
	/// <param name="experience"></param>
	public void AddExperience(float experience)
	{
		Experience += experience;
		if (Experience >= ExperienceToNextLevel)
		{
			IncreaseLevel();
			Experience -= ExperienceToNextLevel;
			ExperienceToNextLevel *= 1.1f;
		}
	}

	/// <summary>
	/// Gets the player's experience.
	/// </summary>
	/// <returns></returns>
	public float getExperience()
	{
		return Experience;
	}

	/// <summary>
	/// Sets the player's experience.
	/// </summary>
	public void IncreaseLevel()
	{
		Level++;
	}

	/// <summary>
	/// Gets the player's level.
	/// </summary>
	/// <returns></returns>
	public int getLevel()
	{
		return Level;
	}
}
