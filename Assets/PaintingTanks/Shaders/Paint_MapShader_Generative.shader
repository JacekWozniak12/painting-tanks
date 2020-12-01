Shader "Custom/Paint_MapShader_Generative" 
{
    //todo    
    Properties
    {
        // Object textures
        [Header(Object)][Space] 
        _Seed("Normal Depth", Range(0,10000)) = 1

    }
    SubShader{
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM       

        ENDCG  
    }
    Fallback "Diffuse"
}
