Shader "Unlit/ClipShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        ENDHLSL


        Pass
        {

            Stencil
            {
                Ref 1
                Pass replace
            }


            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            struct appdata
            {
                float4 vertex : POSITION;
                //float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
  
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = ComputeScreenPos(o.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 Screen = i.uv.xy / i.uv.w;
                float4 col = tex2D(_MainTex, Screen);
                return col;
            }
            ENDHLSL
        }
    }
}
