Shader "Unlit/DiffusePerVertex"
{
    Properties{
        _DiffuseColor ("_DiffuseColor", Color) = (1, 1, 1, 1)
    }
    
    SubShader
    {
        Pass
        {
            
            //Tags{ "LightModel" = "ForwardBase" }
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            float4 _DiffuseColor;

            struct a2v
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : POSITION;
                float3 color : Color;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.position);
                
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

                //环境光颜色
                float3 ambinet = UNITY_LIGHTMODEL_AMBIENT.xyz;

                float3 lightColor = _LightColor0.xyz;

                float3 diffuseColor = _DiffuseColor * lightColor * max(0, dot(worldLight, worldNormal));
                
                o.color = diffuseColor + ambinet; 
                return o;
            }
            
            float4 frag(v2f i) : SV_Target
            {
                float4 color = float4(i.color, 1);
                return color;
            }
            
            ENDCG
            
        }    
    }
}
