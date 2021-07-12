using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizEventController : MonoBehaviour, ILevelChanger
{
    [SerializeField] private Text questionText;
    [SerializeField] private List<QuizComponentsBase> listOfGameComponents = new List<QuizComponentsBase>();
    [SerializeField] private int currentQuestion = 0;
    [SerializeField] private List<GameObject> optionsGameobject = new List<GameObject>();
    [SerializeField] private GameObject restartLevel;
    private int numberAsked;
    private string clickedObject;

    [SerializeField] private UnityEvent OnAnswerClick = new UnityEvent();

    private void Start()
    {
        ChangeLevel();
    }
    public void ChangeLevel()
    {
        if (currentQuestion <= listOfGameComponents.Count)
        {
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
    }
    public void EasyLevel()
    {
            foreach(GameObject gameObject in optionsGameobject.GetRange(0,3))
            {
                gameObject.SetActive(true);
            }
        optionsGameobject[0].name = listOfGameComponents[currentQuestion].CorrectAnswer;
    }
    public void MediumLevel()
    {
        foreach (GameObject gameObject in optionsGameobject.GetRange(0, 6))
        {
            gameObject.SetActive(true);
        }
        optionsGameobject[5].name = listOfGameComponents[currentQuestion].CorrectAnswer;

    }
    public void HardLevel()
    {
        foreach (GameObject gameObject in optionsGameobject)
        {
            gameObject.SetActive(true);
        }
        optionsGameobject[8].name = listOfGameComponents[currentQuestion].CorrectAnswer;
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
            clickedObject = hit.collider.gameObject.name;
            AnswerCheaker();
        }
    }
    private void AnswerCheaker()
    {
        if (clickedObject == listOfGameComponents[currentQuestion].CorrectAnswer)
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
        else if (clickedObject == restartLevel.name)
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
        if (Input.GetMouseButtonDown(0))
        {
            OnAnswerClick.Invoke();

        }

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

 



