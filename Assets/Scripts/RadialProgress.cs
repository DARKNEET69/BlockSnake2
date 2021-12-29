using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour {
	public Image LoadingBar;
	public float Speed;

	private float currentValue;	
	private AdsManager adMan;

    private void Start()
    {		
		adMan = GameObject.FindGameObjectWithTag("AdsManager").GetComponent<AdsManager>();
	}

    private void OnEnable()
    {
		currentValue = 100f;
	}

    void Update ()
	{
		if (currentValue > 0)
		{
			currentValue -= Speed * Time.deltaTime;
		}
		else
		{
			adMan.ShowAd("video");
		}
		LoadingBar.fillAmount = currentValue / 100;
	}
}
