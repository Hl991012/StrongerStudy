// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/DiffusePerVertex"
{
    Properties{
        _DiffuseColor("DiffuseColor", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    
    SubShader{
        
        Pass{
            
            Tags{ "LightModel" = "ForwardBase" }
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            half4 _Color;

            struct a2v
            {
                float4 position : POSITION;
                half4 color : COLOR0;
                float4 texCoord : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : POSITION;
                half4 color : COLOR0;
            };

            v2f vert(a2v v)
            {
                v2f f;
                f.position = UnityObjectToClipPos(v.position);

                float3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                float3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

                half3 diffuseColor = unity_LightColor0 * _Color * max(0, dot(worldNormal, worldLight));
                
                f.color = half4(diffuseColor * v.color.rgb, 1); 
                return f;
            }
            
            float frag(v2f f) : SV_Target
            {
                return f.color;
            }
            
            ENDCG
            
        }    
    }
}
