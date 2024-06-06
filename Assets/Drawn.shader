Shader "Custom/Drawn"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _EdgeTex("Edge Texture", 2D) = "white" {}
        _PencilTex("Pencil Texture", 2D) = "white" {}
        _EdgeThreshold("Edge Threshold", Range(0,1)) = 0.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows

            sampler2D _MainTex;
            sampler2D _EdgeTex;
            sampler2D _PencilTex;
            float _EdgeThreshold;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_EdgeTex;
                float2 uv_PencilTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Sample the textures
                float4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
                float4 edgeTex = tex2D(_EdgeTex, IN.uv_EdgeTex);
                float4 pencilTex = tex2D(_PencilTex, IN.uv_PencilTex);

                // Apply edge detection
                float edge = step(_EdgeThreshold, edgeTex.r);

                // Combine textures
                float4 pencilEffect = mainTex * edge * pencilTex;

                // Output the result
                o.Albedo = pencilEffect.rgb;
                o.Alpha = pencilEffect.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
