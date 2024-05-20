
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostprocessManagerScript : MonoBehaviour
{
    [SerializeField]
    PostProcessVolume postProcessVolume;
    private PostProcessProfile postProcessProfile;
    private Bloom bloomField;

    private ChromaticAberration chromaticAberration;

    [SerializeField, Header("ê^Ç¡îíÇ…Ç»ÇÈÇ‹Ç≈ÇÃéûä‘")] private float changeTime;

    float bloomCount;
    bool isFlash = false;
    bool isCountMax = false;

    public void SetCAvalue(float value) { chromaticAberration.intensity.value = value; }

    public void OnIsFlash() { isFlash = true; }
    public bool GetCountMax() { return isCountMax; }
    // Start is called before the first frame update
    void Start()
    {
        postProcessProfile = postProcessVolume.profile;
        postProcessProfile.TryGetSettings<Bloom>(out bloomField);
        postProcessProfile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        //if ()
        //{
        //    // depthOfField.focusDistance.value = 0.1f;

        //}
        bloomField.intensity.value = 0.0f;
        chromaticAberration.intensity.value = 0.0f;
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

            float easeT= bloomCount / changeTime;
            bloomField.intensity.value=Mathf.Lerp(0, 50, easeT);
            //bloomField.intensity.value = bloomCount * 50.0f / changeTime;
        }

    }
}
