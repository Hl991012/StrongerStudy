Shader"Study/HalfLambertPerVertex"
{
    Properties{
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
    }

    SubShader{
        Tags{"LightMode" = "ForwardBase"}

        pass{

            CGPROGRAM

            #pragma vertex vert;
            #pragma fragment frag;

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            float4 _DiffuseColor;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertexClipPos : POSITION;
                float3 color : Color;
            };

            v2f vert(a2v a)
            {
                v2f o;

                o.vertexClipPos = UnityObjectToClipPos(a.vertex);
                
                float3 worldNormal = UnityObjectToWorldNormal(a.vertex);
                float3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

                float3 diffuseColor = _DiffuseColor * _LightColor0.rgb * dot(worldNormal, worldLightDir) * 0.5 + 0.5;
                o.color = diffuseColor;

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return float4(i.color, 1);
            }

            ENDCG
        }
    }
}