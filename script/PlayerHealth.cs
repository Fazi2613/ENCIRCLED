using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // A jatekos maximum elete
    public int currentHealth;   // A jatekos pillanatnyi elete
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;  // Az elet maximumra allitasa
        healthBar.setMaxHealth(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Opponent"))
        {
            healthBar.setHealth(currentHealth);
            int damage = Random.Range(10, 18);  // Sebzodes 10-17 kozott
            currentHealth -= damage;  // Az eletero csokkentese "damage" ertekkel
            Debug.Log("Player took " + damage + " damage. Health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Die();  // Ha a jatekos elete egyenlo 0-val vagy 0 ala esik a Die metodus meghivodik
            }
        }
    }

    private void Die()
    {
        // A halal scene meghivodik amint a jatekos meghal
        SceneManager.LoadScene("Death");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("DIED");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene("Encircled_map_V1");  // A jelenlegi scene ujranyitasa
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Main Menu");  // A fomenu scene megnyitasa
    }

}