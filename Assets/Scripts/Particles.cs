using UnityEngine;

[System.Serializable]
public struct ParticleObject
{
    [SerializeField] Particles[] _particles;

    public Particles GetParticleByName(string name) {
        foreach (var p in _particles) {
            if (p.Name == name) {
                return p;
            }
        }

        return new Particles();
    }
}

[System.Serializable]
public struct Particles
{
    [SerializeField] private string _name;
    [SerializeField] private ParticleSystem _particle;

    // Optionally, you can add getter methods if needed
    public string Name => _name;
    public ParticleSystem Particle => _particle;
}
