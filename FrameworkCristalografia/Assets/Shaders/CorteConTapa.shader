Shader "CorteConTapa"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CutPlanePos ("Plane Position", Vector) = (0, 0, 0, 0)
        _CutPlaneNormal ("Plane Normal", Vector) = (0, 1, 0, 0)
        _Color ("Color", Color) = (1,1,1,1)
        _CapColor ("Cap Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Primera parte: dibuja el objeto con corte
        Pass
        {
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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

            sampler2D _MainTex;
            float4 _CutPlanePos;
            float4 _CutPlaneNormal;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Calcula la distancia del punto al plano
                float distance = dot(_CutPlaneNormal.xyz, i.worldPos - _CutPlanePos.xyz);

                // Clip si el fragmento está por encima del plano
                clip(distance);

                half4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }

        // Segunda parte: dibuja la tapa en la sección cortada
        Pass
        {
            Stencil
            {
                Ref 1
                Comp equal
            }

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

            fixed4 _CapColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 fragCap (v2f i) : SV_Target
            {
                return _CapColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
