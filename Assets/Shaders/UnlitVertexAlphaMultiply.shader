Shader "Custom/Unlit Vertex Alpha Multiply"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
    }

        SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100

        Cull Off
        ZWrite Off
        Blend DstColor Zero

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

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                i.color.a = i.color.r * 0.3 + i.color.g * 0.59 + i.color.b * 0.11;
                return i.color;
            }
            ENDCG
        }
    }
}