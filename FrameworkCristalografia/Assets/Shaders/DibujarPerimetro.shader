Shader "DibujarPerimetro"
{
    Properties
    {
        _CutPlanePos ("Plane Position", Vector) = (0, 0, 0, 0)
        _CutPlaneNormal ("Plane Normal", Vector) = (0, 1, 0, 0)
        _CapColor ("Cap Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Primera pasada: marcar en el stencil solo la intersección del objeto con el plano
        Pass
        {
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragStencil
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            float4 _CutPlanePos;
            float4 _CutPlaneNormal;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.uv;
                return o;
            }

            half4 fragStencil (v2f i) : SV_Target
            {
                // Calcula la distancia del punto al plano
                float distance = dot(_CutPlaneNormal.xyz, i.worldPos - _CutPlanePos.xyz);

                // Solo marca el stencil si está muy cerca del plano (intersección)
                if (abs(distance) < 0.001)
                {
                    // Devuelve un valor arbitrario para marcar el stencil
                    return half4(0, 0, 0, 1);
                }

                // No marcar nada si no está en la intersección
                discard;
                return half4(0, 0, 0, 0);
            }
            ENDCG
        }

        // Segunda pasada: dibuja solo la tapa en la región marcada por el stencil
        Pass
        {
            Stencil
            {
                Ref 1
                Comp equal // Solo dibuja donde el stencil coincide con el valor establecido
            }
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragCap
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _CutPlanePos;
            float4 _CutPlaneNormal;
            fixed4 _CapColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 fragCap (v2f i) : SV_Target
            {
                // Aquí puedes agregar un cálculo para texturizar la tapa si lo deseas
                return _CapColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
