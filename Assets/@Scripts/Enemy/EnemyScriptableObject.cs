using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Scriptable Object for Enemy that holds the BASE STATS. These can then be modified at object creation time to buff up enemies
/// and to reset their stats if they died or were modified at runtime.
/// </summary>
[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy/Make new Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
  public int health = 100;
  
  public float aiUpdateInterval = 0.1f;
  
  public float acceleration = 8.0f;
  public float angularSpeed = 120.0f;
  public int areaMask = -1;
  public int avoidancePriority = 50;
  public float baseOffset = 0.0f;
  public float height = 2.0f;
  public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
  public float radius = 0.5f;
  public float speed = 3.0f;
  public float stoppingDistance = 0.5f;
  
}
