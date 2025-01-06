Shader"Unlit/Vertex Color + Texture Mask"
{
    Properties
    {
        _GammaValue ("Gamma Value", Range(1.0, 3.0)) = 2.2
        _Texture ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uvTex : TEXCOORD0;
                float2 uvMask : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uvTex : TEXCOORD0;
                float2 uvMask : TEXCOORD1;
            };

            float _GammaValue;
            sampler2D _Texture;
            float4 _Texture_ST;
            sampler2D _Mask;
            float4 _Mask_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply gamma correction to the vertex color
                o.color = pow(v.color, _GammaValue);
                o.uvTex = TRANSFORM_TEX(v.uvTex, _Texture);
                o.uvMask = TRANSFORM_TEX(v.uvMask, _Mask);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_Texture, i.uvTex);
                fixed4 mask = tex2D(_Mask, i.uvMask);
                // Use the red channel of the mask as the brightness
                float brightness = dot(mask.rgb, float3(0.299, 0.587, 0.114));
                return lerp(tex, i.color, brightness);
            }

            ENDCG
        }
    }
}