

Shader"Unlit/PracticeShader"
{
    Properties
    {
        _MainTexture ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _AnimationSpeed ("Animation Speed", Range(0,3)) = 0
        _Offset ("Offset", Range(0,10)) = 0
        [KeywordEnum(Off, On)]
        _Multiply("Color Options", Float) = 0

    }
    SubShader
    {
       Tags { "Queue"="Geometry"}

            
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc

            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
            
            };


            //Connection Variables
            #pragma multi_compile _MULTIPLY_OFF _MULTIPLY_ON
            fixed4 _Color;
            sampler2D _MainTexture;
            float _AnimationSpeed;
            float _Offset;
            

            //Places Vertex in camera space
            v2f vertexFunc(appdata IN)
            {
                v2f OUT;
                IN.vertex.x += sin(_Time.y * _AnimationSpeed + IN.vertex.y + _Offset);
                OUT.position = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                return OUT;
            }

            //Outputs color
            fixed4 fragmentFunc(v2f IN) : SV_Target
            {
                fixed4 pixelColor = tex2D(_MainTexture, IN.uv);
                #if _MULTIPLY_ON
                return pixelColor * _Color;
                #elif _MULTIPLY_OFF
                return pixelColor;
                #endif
               
                
            }


            ENDCG
        }
    }
}
