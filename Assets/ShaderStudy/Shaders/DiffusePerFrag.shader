Shader"Unit/DiffusePerFrag"
{
    Properties{
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)    
    }    
    SubShader{
        Tags{"LightMode" = "ForwardBase"}
        
        Pass{
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

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
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 diffuseColor = _DiffuseColor * _LightColor0.rgb * max(0, dot(i.normal, normalize(_WorldSpaceLightPos0.xyz)));
                return float4(diffuseColor, 1);
            }
            
            ENDCG
        }
    }
}