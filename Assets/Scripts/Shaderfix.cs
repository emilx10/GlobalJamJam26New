using UnityEngine;

public class Shaderfix : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalFloat("_GlobalUnscaledTime", Time.unscaledTime);
    }
}
