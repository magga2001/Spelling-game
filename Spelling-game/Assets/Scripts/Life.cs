using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    [SerializeField] private Sprite activeLife;
    [SerializeField] private Sprite deadLife;
    [SerializeField] private int number_of_lives;
    // Start is called before the first frame update
    void Start()
    {
        number_of_lives = lives.Length;

        for(int i = 0; i < number_of_lives; i++)
        {
            lives[i].sprite = activeLife;
        }
    }

    public void updateLife(int lives)
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
