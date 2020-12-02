Shader "Custom/Paint_BlendShader_Surface" 
{
    Properties
    {
        // Object textures
        [Header(Object)][Space] 
        _ColorModifier("Color", Color) = (0,0,0,0)
        _MainTex("Main Texture", 2D) = "white" {}
        [Normal]
        _BumpTex("Normal Texture", 2D) = "bump" {}
        // _SpecularTex("Specular Texture", 2D) = "specular" {}

        // Standard Shader Unity Features
        _BumpDepth("Normal Depth", Range(0,1)) = 1
        _Metallic("Metallic", Range(0, 1)) = 0
        _Glossiness("Glossiness", Range(0, 1)) = 0

        // Gameplay driven textures
        [Header(Gameplay)][Space] 
        _Influence("Influence", Range(0,1)) = 0.0
        
        
        [NoScaleOffset]
        [PerRendererData]      
        _MaskTex("Mask Texture", 2D) = "white" {} 

        // Debug
        [Header(Debug)][Space] 
        [KeywordEnum(UV0, UV2)] _UVType ("UV Type", Float) = 0
        _TestTex("Test Texture", 2D) = "white" {}
    }
    SubShader{
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM       
        #pragma surface surf Standard fullforwardshadows //vertex:vert
        #pragma target 3.0
        #pragma multi_compile _UVTYPE_UV0 _UVTYPE_UV2 
        
        #include "UnityCG.cginc"
        #include "UnityLightingCommon.cginc"
        #include "AutoLight.cginc"

        sampler2D _MaskTex;
        sampler2D _MainTex;
        sampler2D _BumpTex;
        sampler2D _TestTex;

        float _BumpDepth; 
        float _Influence;
        float _BrushInfluence;

        half _Glossiness;
        half _Metallic;
        half _ColorModifier;

        struct Input {
            float2 uv_MainTex : TEXDOORD0;
            float2 uv2_MaskTex : TEXDOORD2;
            float4 screenPos;
        };
        
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            
            half4 main_color = tex2D(_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal (tex2D (_BumpTex, IN.uv_MainTex) );    

            #if _UVTYPE_UV2
                main_color = tex2D(_TestTex, IN.uv2_MaskTex);
                o.Normal = UnpackNormal (tex2D (_BumpTex, IN.uv2_MaskTex) );
            #endif

            half4 paint = tex2D(_MaskTex, IN.uv2_MaskTex);
     
            o.Normal -= _BumpDepth;

            o.Albedo = main_color.rgb + _ColorModifier - paint.a * _Influence;
            o.Albedo += paint.rgb * paint.a * _Influence;
            
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = main_color.a;
        }
        ENDCG  
    }
    Fallback "Diffuse"
}