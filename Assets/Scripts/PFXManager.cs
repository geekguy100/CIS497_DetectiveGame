using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFXManager : Singleton<PFXManager>
{
    public ParticleSystem pickup;
    public ParticleSystem dust;

    public void SpawnParticle(string particleType, Vector3 pos)
    {
        ParticleSystem particle = null;
        switch (particleType)
        {
            case "pickup":
                particle = pickup;
                break;
            case "dust":
                particle = dust;
                break;
            default:
                Debug.LogWarning("Particle type \'" + particleType + "\' not found!");
                return;
        }

        particle = Instantiate(particle, pos, particle.transform.rotation);
        particle.Play();
        Destroy(particle.gameObject, 1f);
    }
}
