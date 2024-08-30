Shader "Shaders/Interseccion" {

    Properties {

        _MainTex ("Texture", 2D) = "white" {}
        _PlaneOffset ("Plane Offset", Float) = 0.0
    }
    SubShader {

        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;
        float _PlaneOffset;

        struct Input {
            float3 worldPos;
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            
            if (IN.worldPos.y > _PlaneOffset) {
                clip(-1);
            }
            
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    } 
    FallBack "Diffuse"
    
}