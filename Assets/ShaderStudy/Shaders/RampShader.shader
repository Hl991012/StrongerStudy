Shader "Study/RampShader"
{
    Properties
    {
        _RampTex ("Ramp", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Specular("Specular", Color) = (1, 1, 1, 1)
        _Gloss("Gloss", float) = 1
    }
    SubShader
    {
        Tags { "LightModel"="ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            sampler2D _RampTex;
            float4 _Color;
            float4 _RampTex_ST;
            float4 _Specular;
            float _Gloss;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float2 texCoord : TEXCOORD0;
            };

            struct v2f  
            {
                float4 svPos : SV_POSITION;
                float4 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };
            

            v2f vert (appdata v)
            {
                v2f o;
                o.svPos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
                o.uv = TRANSFORM_TEX(v.texCoord, _RampTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                float3 worldViewDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                

                //计算漫反射颜色
                float halfLambert = 0.5f + dot(worldLightDir, i.worldNormal) * 0.5f;
                float3 diffuseColor = tex2D(_RampTex, float2(halfLambert, halfLambert)).rgb * _Color.rgb;
                float3 diffuse = _LightColor0.rgb * diffuseColor;

                //计算高光颜色
                float3 halfDir = normalize(worldLightDir + worldViewDir);
                float3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(i.worldNormal, halfDir)), _Gloss);

                float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
                
                return float4(diffuse + specular + ambient, 0.5f);
            }
            ENDCG
        }
    }
}
