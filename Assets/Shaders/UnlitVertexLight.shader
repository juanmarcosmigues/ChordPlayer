Shader "Custom/Unlit Vertex Add"
{
    Properties
    {
        _GammaValue("Gamma Value", Range(1.0, 3.0)) = 2.2
        _AlphaValue("Alpha Value", Range(0.0, 3.0)) = 1.0
    }

        SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100

        Cull Off
        ZWrite Off
        Blend SrcColor One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 color : COLOR;
                float4 vertex : POSITION;
            };

            struct v2f
            {
                fixed4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            float _GammaValue;
            float _AlphaValue;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply gamma correction to the vertex color
                o.color = pow(v.color, _GammaValue);

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float a = i.color.r * 0.3 + i.color.g * 0.59 + i.color.b * 0.11;
                i.color.a = a * _AlphaValue;
                return i.color;
            }
            ENDCG
        }
    }
}