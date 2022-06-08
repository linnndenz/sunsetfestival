using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public bool isAutumn;
    public Transform directionLight;
    Light m_light;
    public Material skyMat;
    public float duration = 10;//变化时间（秒）
    [Header("白天")]
    public Material dayMat;
    public float dayAmbientIntensity = 1;
    public float dayReflectionIntensity = 1;
    public float dayLightIntensity = 1;
    [Header("日落")]
    public Material sunsetMat;
    public float sunsetAmbientIntensity = 1;
    public float sunsetReflectionIntensity = 1;
    public float sunsetLightIntensity = 1;
    [Header("夜晚")]
    public Material nightMat;
    public float nightAmbientIntensity = 0;
    public float nightLightIntensity = 0;
    public float nightReflectionIntensity = 0;

    //dotween
    Sequence anims;
    void Start()
    {
        DOTween.KillAll();
        //初始化
        RenderSettings.skybox = skyMat;
        directionLight.localEulerAngles = new Vector3(85,directionLight.localEulerAngles.y,directionLight.localEulerAngles.z);
        m_light = directionLight.GetComponent<Light>();
        m_light.intensity = dayLightIntensity;
        Day();

        anims = DOTween.Sequence();
        //to sunset
        anims.Append(DOTween.To(() => RenderSettings.ambientIntensity, x => RenderSettings.ambientIntensity = x, sunsetAmbientIntensity, duration / 2));
        anims.Join(DOTween.To(() => RenderSettings.reflectionIntensity, x => RenderSettings.reflectionIntensity = x, sunsetReflectionIntensity, duration / 2));
        anims.Join(skyMat.DOColor(sunsetMat.GetColor("_SunDiscColor"), "_SunDiscColor", duration / 2));
        anims.Join(skyMat.DOFloat(sunsetMat.GetFloat("_SunDiscMultiplier"), "_SunDiscMultiplier", duration / 2));
        anims.Join(skyMat.DOFloat(sunsetMat.GetFloat("_SunDiscExponent"), "_SunDiscExponent", duration / 2));

        anims.Join(skyMat.DOColor(sunsetMat.GetColor("_SunHaloColor"), "_SunHaloColor", duration / 2));
        anims.Join(skyMat.DOFloat(sunsetMat.GetFloat("_SunHaloExponent"), "_SunHaloExponent", duration / 2));
        anims.Join(skyMat.DOFloat(sunsetMat.GetFloat("_SunHaloContribution"), "_SunHaloContribution", duration / 2));

        anims.Join(skyMat.DOColor(sunsetMat.GetColor("_SkyGradientTop"), "_SkyGradientTop", duration / 2));
        anims.Join(skyMat.DOColor(sunsetMat.GetColor("_SkyGradientBottom"), "_SkyGradientBottom", duration / 2));
        anims.Join(skyMat.DOColor(sunsetMat.GetColor("_HorizonLineColor"), "_HorizonLineColor", duration / 2));
        anims.Join(DOTween.To(() => m_light.intensity, x => m_light.intensity = x, sunsetLightIntensity, duration / 2));

        //to night
        anims.Append(DOTween.To(() => RenderSettings.ambientIntensity, x => RenderSettings.ambientIntensity = x, nightAmbientIntensity, duration / 2));
        anims.Join(DOTween.To(() => RenderSettings.reflectionIntensity, x => RenderSettings.reflectionIntensity = x, nightReflectionIntensity, duration / 2));
        anims.Join(skyMat.DOColor(nightMat.GetColor("_SunDiscColor"), "_SunDiscColor", duration / 2));
        anims.Join(skyMat.DOFloat(nightMat.GetFloat("_SunDiscMultiplier"), "_SunDiscMultiplier", duration / 2));
        anims.Join(skyMat.DOFloat(nightMat.GetFloat("_SunDiscExponent"), "_SunDiscExponent", duration / 2));

        anims.Join(skyMat.DOColor(nightMat.GetColor("_SunHaloColor"), "_SunHaloColor", duration / 2));
        anims.Join(skyMat.DOFloat(nightMat.GetFloat("_SunHaloExponent"), "_SunHaloExponent", duration / 2));
        anims.Join(skyMat.DOFloat(nightMat.GetFloat("_SunHaloContribution"), "_SunHaloContribution", duration / 2));

        anims.Join(skyMat.DOColor(nightMat.GetColor("_SkyGradientTop"), "_SkyGradientTop", duration / 2));
        anims.Join(skyMat.DOColor(nightMat.GetColor("_SkyGradientBottom"), "_SkyGradientBottom", duration / 2));
        anims.Join(skyMat.DOColor(nightMat.GetColor("_HorizonLineColor"), "_HorizonLineColor", duration / 2));
        anims.Join(DOTween.To(() => m_light.intensity, x => m_light.intensity = x, nightLightIntensity, duration / 2));

        //to day
        anims.Append(DOTween.To(() => RenderSettings.ambientIntensity, x => RenderSettings.ambientIntensity = x, dayAmbientIntensity, duration));
        anims.Join(DOTween.To(() => RenderSettings.reflectionIntensity, x => RenderSettings.reflectionIntensity = x, dayReflectionIntensity, duration));
        anims.Join(skyMat.DOColor(dayMat.GetColor("_SunDiscColor"), "_SunDiscColor", duration));
        anims.Join(skyMat.DOFloat(dayMat.GetFloat("_SunDiscMultiplier"), "_SunDiscMultiplier", duration / 2));
        anims.Join(skyMat.DOFloat(dayMat.GetFloat("_SunDiscExponent"), "_SunDiscExponent", duration / 2));

        anims.Join(skyMat.DOColor(dayMat.GetColor("_SunHaloColor"), "_SunHaloColor", duration));
        anims.Join(skyMat.DOFloat(dayMat.GetFloat("_SunHaloExponent"), "_SunHaloContribution", duration / 2));
        anims.Join(skyMat.DOFloat(dayMat.GetFloat("_SunHaloContribution"), "_SunHaloContribution", duration / 2));

        anims.Join(skyMat.DOColor(dayMat.GetColor("_SkyGradientTop"), "_SkyGradientTop", duration));
        anims.Join(skyMat.DOColor(dayMat.GetColor("_SkyGradientBottom"), "_SkyGradientBottom", duration));
        anims.Join(skyMat.DOColor(dayMat.GetColor("_HorizonLineColor"), "_HorizonLineColor", duration));
        anims.Join(DOTween.To(() => m_light.intensity, x => m_light.intensity = x, dayLightIntensity, duration));
       

        anims.SetLoops(-1);
        anims.Play();

    }

    void Update()
    {
        directionLight.Rotate(-Time.deltaTime * 360 / (duration * 2), 0, 0, Space.Self);
    }

    public void Day()
    {
        RenderSettings.reflectionIntensity = dayReflectionIntensity;
        RenderSettings.ambientIntensity = dayAmbientIntensity;
        skyMat.SetColor("_SunDiscColor", dayMat.GetColor("_SunDiscColor"));
        skyMat.SetFloat("_SunDiscMultiplier", dayMat.GetFloat("_SunDiscMultiplier"));
        skyMat.SetFloat("_SunDiscExponent", dayMat.GetFloat("_SunDiscExponent"));

        skyMat.SetColor("_SunHaloColor", dayMat.GetColor("_SunHaloColor"));
        skyMat.SetFloat("_SunHaloExponent", dayMat.GetFloat("_SunHaloExponent"));
        skyMat.SetFloat("_SunHaloContribution", dayMat.GetFloat("_SunHaloContribution"));

        skyMat.SetColor("_SkyGradientTop", dayMat.GetColor("_SkyGradientTop"));
        skyMat.SetColor("_SkyGradientBottom", dayMat.GetColor("_SkyGradientBottom"));
        skyMat.SetColor("_HorizonLineColor", dayMat.GetColor("_HorizonLineColor"));
    }

    public void Sunset()
    {
        RenderSettings.reflectionIntensity = sunsetReflectionIntensity;
        RenderSettings.ambientIntensity = sunsetAmbientIntensity;
        skyMat.SetColor("_SunDiscColor", sunsetMat.GetColor("_SunDiscColor"));
        skyMat.SetFloat("_SunDiscMultiplier", sunsetMat.GetFloat("_SunDiscMultiplier"));
        skyMat.SetFloat("_SunDiscExponent", sunsetMat.GetFloat("_SunDiscExponent"));

        skyMat.SetColor("_SunHaloColor", sunsetMat.GetColor("_SunHaloColor"));
        skyMat.SetFloat("_SunHaloExponent", sunsetMat.GetFloat("_SunHaloExponent"));
        skyMat.SetFloat("_SunHaloContribution", sunsetMat.GetFloat("_SunHaloContribution"));

        skyMat.SetColor("_SkyGradientTop", sunsetMat.GetColor("_SkyGradientTop"));
        skyMat.SetColor("_SkyGradientBottom", sunsetMat.GetColor("_SkyGradientBottom"));
        skyMat.SetColor("_HorizonLineColor", sunsetMat.GetColor("_HorizonLineColor"));
    }

    void OnDestroy()
    {
        if (isAutumn) {
            Sunset();
        } else {
            Day();
        }
    }

}
