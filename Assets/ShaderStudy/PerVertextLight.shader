Shader"Study/PerVertextLight"
{
    Properties{
        _DiffuseColor("漫反射颜色", Color) = (1,1,1,1)
        _Specular("高光颜色", Color) = (1,1,1,1)
        _Gloss("光泽度", float) = 1
    }
    
    SubShader
    {
        Tags{"LightMode" = "ForwardBase"}
        
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            float4 _DiffuseColor;
            float4 _Specular;
            float _Gloss;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertexClipPos : POSITION;
                float3 normal : TEXCORRD0;
            };

            v2f vert(a2v v)
            {
                v2f o;

                o.vertexClipPos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                
                float3 diffuseColor = _DiffuseColor * _LightColor0.rgb * max(0, dot(i.normal, worldLightDir));

                float3 reflectionDir = normalize(reflect(-worldLightDir, i.normal));

                float3 specular = _Specular.rgb * _LightColor0.rgb * pow(max(0, dot(i.normal, reflectionDir)), _Gloss);
                
                return float4(diffuseColor + UNITY_LIGHTMODEL_AMBIENT.rgb + specular, 1);
            }
            
            ENDCG
        }
    }
}