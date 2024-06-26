using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AudioListener))]
public class GetSpectrumDataExample : MonoBehaviour
{
    public float t = 1;
    float _lastLow = 0;
    public float rangeSize;
    public int senceStart;

    PostprocessManagerScript postprocess;
    [SerializeField] private GameObject targetObj;

    private void Start()
    {
        postprocess = FindAnyObjectByType<PostprocessManagerScript>();

    }
    void Update()
    {
        float[] spectrum = new float[64];

    AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Triangle);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }

       
        float low = 0;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            low++;
        }
        for (int i = senceStart; i < senceStart+2; i++)
        {
            low += spectrum[i];
        }
            
        low = _lastLow * (1 - t) + low * t;
        transform.localScale = Vector3.one * rangeSize * low + Vector3.one;
        targetObj.transform.localScale = Vector3.one*2+transform.localScale;
        float effectValue = Mathf.Clamp(low * rangeSize, 0,1);
        postprocess.SetCAvalue(effectValue);
        _lastLow = low;

    }
}