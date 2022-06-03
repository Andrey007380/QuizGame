using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class QuizEventController : MonoBehaviour, ILevelChanger
{
    [SerializeField] private Text questionText;
    [SerializeField] private List<QuizComponentsBase> listOfGameComponents;
    [SerializeField] private int currentQuestion = 0;
    [SerializeField] private List<GameObject> optionsGameobject;
    [SerializeField] private GameObject restartLevel;
    private int _numberAsked;
    private GameObject _clickedObject;

    [SerializeField] private UnityEvent<GameObject> onAnswerClick;

    private void Start()
    {
        ChangeLevel();
    }
    public void ChangeLevel()
    {
        if (currentQuestion > listOfGameComponents.Count) 
            return;
        
        questionText.text = listOfGameComponents[currentQuestion].Question;

        switch (currentQuestion)
        {
            case 0:
                EasyLevel();
                break;
            case 1:
                MediumLevel();
                break;
            case 2:
                HardLevel();
                break;
        }
    }
    public void EasyLevel()
    {
            foreach(GameObject gameObject in optionsGameobject.GetRange(0,3))
            {
                gameObject.SetActive(true);
            }

            var correctAnswer = listOfGameComponents[currentQuestion].CorrectAnswer;
    }
    public void MediumLevel()
    {
        foreach (GameObject gameObject in optionsGameobject.GetRange(0, 6))
        {
            gameObject.SetActive(true);
        }
        var correctAnswer =listOfGameComponents[currentQuestion].CorrectAnswer;

    }
    public void HardLevel()
    {
        foreach (GameObject gameObject in optionsGameobject)
        {
            gameObject.SetActive(true);
        }
        var correctAnswer = listOfGameComponents[currentQuestion].CorrectAnswer;
    }
    public void RestartLevel()
    {
        foreach (GameObject gameObject in optionsGameobject)
        {
            gameObject.SetActive(false);
        }
        restartLevel.SetActive(false);
        currentQuestion = 0;
        ChangeLevel();        
    }
    public void Click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) 
        {
            _clickedObject = hit.collider.gameObject;
            AnswerCheaker(_clickedObject);
        }
    }
    public void AnswerCheaker(GameObject clicked)
    {
        if (_clickedObject == listOfGameComponents[currentQuestion].CorrectAnswer)
        {
            Debug.Log("Correct");
            if (currentQuestion < listOfGameComponents.Capacity - 1)
            {
                currentQuestion++;
                ChangeLevel();
            }
            else
            {
                End();
            }

        }
        else if (_clickedObject == restartLevel)
        {
            RestartLevel();        
        }
        else
        {
            Debug.Log("Wrong");
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Click();
    }

    public void End()
    {
        foreach (GameObject gameObject in optionsGameobject)
        {
            gameObject.SetActive(false);
        }
        restartLevel.gameObject.SetActive(true);
    }
    
}

 



