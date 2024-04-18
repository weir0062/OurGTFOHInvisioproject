Shader "Hidden/PencilEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionSpeed ("Distortion Speed", Range(0, 10)) = 1
        _DistortionAmount ("Distortion Amount", Range(0, 0.5)) = 0.1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _DistortionSpeed;
            float _DistortionAmount;

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y * _DistortionSpeed;
                float2 uv = i.uv + float2(sin(time), cos(time)) * _DistortionAmount;
                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
