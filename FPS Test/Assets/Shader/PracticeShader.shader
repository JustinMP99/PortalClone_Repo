

Shader"Unlit/PracticeShader"
{
    Properties
    {
        _MainTexture ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _AnimationSpeed ("Animation Speed", Range(0,3)) = 0
        _Offset ("Offset", Range(0,10)) = 0

    }
    SubShader
    {
       

            
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
            
            fixed4 _Color;
            sampler2D _MainTexture;
            float _AnimationSpeed;
            float _Offset;
            

            //Places Vertex in camera space
            v2f vertexFunc(appdata IN)
            {
                v2f OUT;
               
                OUT.position = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                return OUT;
            }

            //Outputs color
            fixed4 fragmentFunc(v2f IN) : SV_Target
            {
                fixed4 pixelColor = tex2D(_MainTexture, IN.uv);
                return pixelColor;
            }


            ENDCG
        }
    }
}
