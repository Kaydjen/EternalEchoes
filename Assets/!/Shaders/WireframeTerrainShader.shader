Shader "Custom/WireframeTerrain"
{
    Properties
    {
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _BackgroundColor ("Background Color", Color) = (0,0,0,1)
        _LineWidth ("Line Width", Range(0, 0.5)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2g
            {
                float4 vertex : POSITION;
            };

            struct g2f
            {
                float4 vertex : SV_POSITION;
                float3 barycentric : TEXCOORD0;
            };

            float4 _LineColor;
            float4 _BackgroundColor;
            float _LineWidth;

            v2g vert (appdata v)
            {
                v2g o;
                o.vertex = v.vertex;
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
            {
                g2f o;
                o.barycentric = float3(1, 0, 0);
                o.vertex = UnityObjectToClipPos(IN[0].vertex);
                triStream.Append(o);

                o.barycentric = float3(0, 1, 0);
                o.vertex = UnityObjectToClipPos(IN[1].vertex);
                triStream.Append(o);

                o.barycentric = float3(0, 0, 1);
                o.vertex = UnityObjectToClipPos(IN[2].vertex);
                triStream.Append(o);
            }

            float4 frag (g2f i) : SV_Target
            {
                float3 barys = i.barycentric;
                float3 deltas = fwidth(barys);
                float3 smoothing = deltas * 1.0;
                float3 thickness = deltas * _LineWidth;

                barys = smoothstep(thickness, thickness + smoothing, barys);
                float minBary = min(barys.x, min(barys.y, barys.z));
                return lerp(_LineColor, _BackgroundColor, minBary);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}