Shader "Clipping" {

    Properties {
        
        _MainTex ("Texture", 2D) = "white" {}
        _CubeCenter ("Cube Center", Vector) = (0, 0, 0)
        _CubeSize ("Cube Size", Vector) = (1, 1, 1)
        _Transparency ("Transparency", Range(0, 1)) = 0.5
    }

    SubShader {

        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _CubeCenter;
            float4 _CubeSize;
            float _Transparency;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Verifica si el punto está dentro del cubo
                float3 minBound = _CubeCenter.xyz - _CubeSize.xyz / 2.0;
                float3 maxBound = _CubeCenter.xyz + _CubeSize.xyz / 2.0;

                if (i.worldPos.x < minBound.x || i.worldPos.x > maxBound.x ||
                    i.worldPos.y < minBound.y || i.worldPos.y > maxBound.y ||
                    i.worldPos.z < minBound.z || i.worldPos.z > maxBound.z) {
                    discard;  // No renderizar fuera del cubo
                }

                // Renderiza la textura con transparencia
                fixed4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency; // Aplica la transparencia
                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}
