Shader "Unlit/ClipShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Geometry"
            "RenderType"="Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }
        //LOD 100
        HLSLINCLUDE
        //#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "UnityCG.cginc"
        ENDHLSL


        Pass
        {
            Name "Mask" 

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
               
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 ScreenPos : TEXCOORD0;
  
            };

            uniform sampler2D _MainTex;
            //float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.ScreenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 uv = i.ScreenPos.xy / i.ScreenPos.w;
                float4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDHLSL
        }
    }
}
