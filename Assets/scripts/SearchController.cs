using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SearchController : MonoBehaviour
{
    private int num;

    //private int countGuess;

    [SerializeField]
    private InputField input;

    [SerializeField]
    private Text text;


    void Awake()
    {
        num = Random.Range(0, 100);
        text.text = "Guess A Number Between 0 and 100";
       // input = GameObject.Find("InputField").GetComponent<InputField>();
    }

    public void GetInput(string guess)
    {
        //Debug.Log("You entered " + guess);
        ComparedGuesses(int.Parse(guess));
        input.text = "";
    }

    void ComparedGuesses(int guess)
    {
        if (guess == num)
        {
            text.text = "You Guessed Correctly The Number Was " + guess;
        }
        else if(guess<num)
        {
            text.text = "Your Guess Number is Less Than The Number You Are Trying To Guess";
        }
        else if(guess> num)
        {
            text.text = "Your Guess Is Greater Than The Number You Are Trying To Guess";
        }
    }
}
