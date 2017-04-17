using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class LanternaBehaviour : MonoBehaviour {
    
    public KeyCode interruptor;
    public KeyCode luzForte;

    bool isLuzForte;

    Light lanterna;

    public AudioClip somClick;

    AudioSource click;

    [Range(1, 200)]
    public float cargaMaxima = 100;
    float cargaAtual;
    float countDown = 10;

    public Image lightBar;
    public Image lanternaIcon;

    public Sprite onFlashlight;
    public Sprite offFlashlight;

    public Text relogio;

    // Use this for start
    void Start() {
        GameObject LanternaGO = GameObject.Find("Lanterna");
        lanterna = LanternaGO.GetComponent<Light>();
        isLuzForte = false;

        click = GetComponent<AudioSource>();

        lanterna.enabled = false;
        
        cargaAtual = cargaMaxima;

        lanternaIcon.sprite = offFlashlight;
	}
	
	// Update is called once per frame
	void Update () {

        updateLightBar();

        updateCarga();

        if (Input.GetKeyDown(interruptor) && cargaAtual > 0)
        {
            ativarInterruptor();
            reproduzirSom();
        }

        if (Input.GetKeyDown(luzForte))
        {
            ativaLuzForte();
        }

        trocaIconeLanterna();
        timeLeft();
        deadByTime();
        Debug.Log("Caraga : "+cargaAtual);
    }

    void ativarInterruptor() // Change state of light component
    {
        lanterna.enabled = !lanterna.enabled;
    }
    
    void ativaLuzForte()
    {
        isLuzForte = !isLuzForte;
        if (isLuzForte)
        {
            lanterna.range = 100;
            lanterna.spotAngle = 200;
        }
        else
        {
            lanterna.range = 15;
            lanterna.spotAngle = 80;
        }
    } //Increase range and spotAngle giving a stronger effect to light

    void reproduzirSom()
    {
        click.clip = somClick;
        click.Play();
    } // Plays a click sound

    void updateLightBar()
    {
        lightBar.fillAmount = ((1/cargaMaxima) * cargaAtual);
    }  //A bar with the actual state of flashlight's charge

    void updateCarga()
    {
        if(lanterna.enabled && isLuzForte)
        {
            cargaAtual -= Time.deltaTime * 2f;
        }
        else if (lanterna.enabled)
        {
            cargaAtual -= Time.deltaTime;
        }

        if (cargaAtual <= 0 && lanterna.enabled)
        {
            ativarInterruptor();
            reproduzirSom();
        }
    } // Decrease charte by time

    public void addCarga(float carga)
    {
        float temp;
        temp = cargaAtual + carga;
        if (temp > cargaMaxima) {
            cargaAtual = cargaMaxima;
        }
        else
        {
            cargaAtual += carga;
        }
    }// Increase charge when a specific item is collected 

    void trocaIconeLanterna()
    {
        if (lanterna.enabled)
        {
            lanternaIcon.sprite = onFlashlight;
        }
        else
        {
            lanternaIcon.sprite = offFlashlight;
        }
    } // Change flashlight's icon to on or off

    void timeLeft()
    {
        if(cargaAtual <= 0)
        {
            countDown -= Time.deltaTime;
            relogio.enabled = true;
            relogio.text = countDown.ToString();
        }
        else
        {
            relogio.enabled = false;
            countDown = 10;
        }
    } // Time lefting until die

    void deadByTime()
    {
        if (countDown <= 0)
        { 
            relogio.text = "GameOver";
        }
    } // When time is over, Game Over
}
