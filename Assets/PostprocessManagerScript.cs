
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostprocessManagerScript : MonoBehaviour
{
    [SerializeField]
    PostProcessVolume postProcessVolume;
    private PostProcessProfile postProcessProfile;
    private Bloom bloomField;

    [SerializeField, Header("ê^Ç¡îíÇ…Ç»ÇÈÇ‹Ç≈ÇÃéûä‘")] private float changeTime;

    float bloomCount;
    bool isFlash = false;
    bool isCountMax = false;

    public void OnIsFlash() { isFlash = true; }
    public bool GetCountMax() { return isCountMax; }
    // Start is called before the first frame update
    void Start()
    {
        postProcessProfile = postProcessVolume.profile;
        postProcessProfile.TryGetSettings<Bloom>(out bloomField);
        //if ()
        //{
        //    // depthOfField.focusDistance.value = 0.1f;

        //}
        bloomField.intensity.value = 0.0f;
        isFlash = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isFlash)
        {
            if (bloomCount > changeTime)
            {
                isCountMax = true;
                return;
            }
            bloomCount += Time.deltaTime;

            bloomField.intensity.value = bloomCount * 30.0f / changeTime;
        }

    }
}
