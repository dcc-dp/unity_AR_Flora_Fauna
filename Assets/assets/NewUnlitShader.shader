Shader "Custom/UIGradientShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _TopColor;
            fixed4 _BottomColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Linear interpolation between top and bottom color based on y-coordinate
                fixed4 col = lerp(_BottomColor, _TopColor, i.uv.y);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
