using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSpaceAnimation : MonoBehaviour
{
    [SerializeField][Range(0f,100f)]
    private float speed = 50;
    
    [SerializeField]
    private List<Image> backgroundImages;
    
    private Queue<Image> _backgroundQueue;
    private Coroutine _bgCoroutine;
   
    void Start()
    {
        _backgroundQueue= new Queue<Image>(backgroundImages);
        _bgCoroutine = StartCoroutine(MoveBackground());
    }
    
    private IEnumerator MoveBackground()
    {
        while (true)
        {
            foreach (Image image in backgroundImages)
            {
                image.rectTransform.anchoredPosition += Vector2.down * (speed * Time.deltaTime);
            }
           
            if (_backgroundQueue.Peek().rectTransform.anchoredPosition.y <= -Screen.height/2)
            {
                Image bg = _backgroundQueue.Dequeue();
                bg.rectTransform.anchoredPosition = new Vector2(0,Screen.height);
                _backgroundQueue.Enqueue(bg);
            }

            yield return null;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_bgCoroutine);
    }
}
