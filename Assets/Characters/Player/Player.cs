using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int healthPoints = 100;
    public int currentHealthPoints = 100;
    [SerializeField] HealthBar healthBar;
    Animator animator;

    public void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;

        if(currentHealthPoints <= 0){
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }

        healthBar.SetBarState(currentHealthPoints, healthPoints);
    }

}
