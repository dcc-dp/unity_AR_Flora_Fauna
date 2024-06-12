using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawabHandler : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Jawab()
    {
        if (isCorrect)
        {
            Debug.Log("Benar");
            quizManager.benar();
        }
        else
        {
            Debug.Log("Salah");
            quizManager.salah();
        }
    }
}
