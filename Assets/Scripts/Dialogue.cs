using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject child;
    public GameObject _camera;
    public string[] lines;
    public float textSpeed;

    private bool theEnd = false;
    private float timer = 4.5f;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (text.text == lines[index])
            {
                NextLine();
            }
            else 
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
        if (theEnd) 
        {
            Debug.Log(timer);
            timer -= Time.deltaTime;
            if (timer < 0) 
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    void StartDialogue() 
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() 
    {
        foreach (char c in lines[index].ToCharArray()) 
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine() 
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else 
        {
            child.gameObject.SetActive(false);
            _camera.GetComponent<Animator>().Play("IntroAnimationPart2");
            theEnd = true;
        }
    }
}
