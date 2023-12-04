Shader"Study/AlphaTest"
{
    Properties{
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("MainTex", 2D) = "White" {}
        _AlphaTest("AlphaTest", Range(0, 1)) = 1
    }
    
    SubShader{
        Tags{"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
        Pass
        {
            CGPROGRAM

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            #pragma vertex vert
            #pragma fragment frag

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _AlphaTest;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texCoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD2;
                float3 worldNormal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2f vert(a2v i)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.worldPos = mul(unity_ObjectToWorld, i.vertex);
                o.worldNormal = UnityObjectToWorldNormal(i.normal);
                o.uv = TRANSFORM_TEX(i.texCoord, _MainTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 worldNormal = normalize(i.worldNormal);
                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                
                float4 texColor = tex2D(_MainTex, i.uv);
                // if((texColor.a - _AlphaTest) < 0)
                // {
                //     discard;
                // }
                clip(texColor.a - _AlphaTest);
                

                float3 albedo = texColor.rgb * _Color;
                float3 diffuse = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));
                
                return float4(diffuse + UNITY_LIGHTMODEL_AMBIENT * albedo,1);
            }
            ENDCG
        }    
    }
}