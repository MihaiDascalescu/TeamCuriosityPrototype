using UnityEngine;

public class GravityParticleSystem : MonoBehaviour
{
    [SerializeField] private GravityCenter center;
    [SerializeField] private Color outsideRangeColor;
    [SerializeField] private Color insideRangeColor;
    void Start()
    {
        center.TargetInRange += ChangeColor;
    }

    private void ChangeColor(object sender, bool inRange)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = inRange ? new ParticleSystem.MinMaxGradient(insideRangeColor) : new ParticleSystem.MinMaxGradient(outsideRangeColor);
    }
}
