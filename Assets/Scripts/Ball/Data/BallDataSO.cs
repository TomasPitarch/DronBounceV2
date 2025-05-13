using UnityEngine;

[CreateAssetMenu(fileName = "BallDataSO", menuName = "Scriptable Objects/BallDataSO")]
public class BallDataSo : ScriptableObject
{
    public int maxAmountOfBalls;
    public float spawnTime;
    public float initialForce;
    public string bounceSound;
}
