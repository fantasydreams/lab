using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSettings
{
    public float angle { get; set; }
    public float radius { get; set; }
    public float speed { get; set; }
    public particleSettings(float r)
    {
        this.radius = r;
        this.angle = Random.value * 2 * Mathf.PI;
        this.speed = Random.value * Mathf.Sqrt(radius);
    }
    public Vector3 getPosition()
    {
        return radius * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }
    public void rotate()
    {
        this.angle += Time.deltaTime * speed / 10;
        if (this.angle > 2 * Mathf.PI)
            this.angle -= 2 * Mathf.PI;
        this.radius += Random.value * 0.2f - 0.1f;
        if (this.radius > par.MaxRadius)
            this.radius = par.MaxRadius;
        if (this.radius < par.MinRadius)
            this.radius = par.MinRadius;
    }
}
public class par : MonoBehaviour {

    private ParticleSystem particle_sys;
    private ParticleSystem.Particle[] particlesArray;
    private particleSettings[] psetting;
    public int seaResolution = 100;
    public static float MaxRadius = 30f;
    public static float MinRadius = 10f;
    public float radius = 30.0f;
    public Gradient colorGradient;


    void setInitialPosition()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                psetting[i * seaResolution + j] = new particleSettings(radius);
                particlesArray[i * seaResolution + j].position = psetting[i * seaResolution + j].getPosition();
            }
        }
        particle_sys.SetParticles(particlesArray, particlesArray.Length);
    }

    void RotateParticles()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                psetting[i * seaResolution + j].rotate();
                particlesArray[i * seaResolution + j].position = psetting[i * seaResolution + j].getPosition();
            }
        }
    }

    void changeColor()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                float value = (Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup));
                value += psetting[i * seaResolution + j].angle / 2 / Mathf.PI;
                while (value > 1)
                    value--;
                particlesArray[i * seaResolution + j].color = colorGradient.Evaluate(value);
                //particlesArray [i * seaResolution + j].color = colorGradient.Evaluate (Random.value);
            }
        }
    }


    void Start()
    {
        particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution];
        psetting = new particleSettings[seaResolution * seaResolution];
        particle_sys = GetComponent<ParticleSystem>();
        var va = particle_sys.main;
        va.maxParticles = seaResolution * seaResolution;
        particle_sys.Emit(seaResolution * seaResolution);
        particle_sys.GetParticles(particlesArray);
        setInitialPosition();

    }
    void Update()
    {
        RotateParticles();
        changeColor();
        particle_sys.SetParticles(particlesArray, particlesArray.Length);
    }

}
