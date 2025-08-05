using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class FreezeCounter : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshProUGUI numberText;
 
    public async Task StartCounter(int time)
    {
        gameObject.SetActive(true);
        
        numberText.text = time.ToString();
        float i= time;

        while(i>0)
        {
            i-=Time.deltaTime;
            string newText = ((int)(i / 1)).ToString();
            await Task.Yield();
            numberText.text= newText;
        }
        
        gameObject.SetActive(false);
    }
   
}
