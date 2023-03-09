using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    [SerializeField] private Sprite activeLife;
    [SerializeField] private Sprite deadLife;
    private int number_of_lives;

    public void SetUp(int max_lives, int number_of_lives)
    {
        this.number_of_lives = number_of_lives;

        for (int i = 0; i < max_lives; i++)
        {
            if(i < number_of_lives)
            {
                lives[i].sprite = activeLife;
            }
            else
            {
                lives[i].sprite = deadLife;
            }
            lives[i].gameObject.SetActive(true);
        }
    }

    public void UpdateLife(int lives)
    {
        if(lives > -1)
        {
            for (int i = 0; i < lives; i++)
            {
                this.lives[i].sprite = activeLife;
            }

            for (int i = lives; i < number_of_lives; i++)
            {
                this.lives[i].sprite = deadLife;
            }
        }
    }
}
