using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface for power ups
/// </summary>
public interface IPowerUp
{
    /// <summary>
    /// Returns id of power up
    /// </summary>
    /// /// <returns>Id of power up</returns>
    public uint GetPowerUpId();

    /// <summary>
    /// Returns duration of power up
    /// </summary>
    /// <returns>Duration of power up</returns>
    public float GetPowerUpDuration();

}
