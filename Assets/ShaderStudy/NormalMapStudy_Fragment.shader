// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader"Study/NormalMap_Fragement"
{
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("MainTex", 2D) = "White" {}
        _BumpMap("BumpMap", 2D) = "bump" {}
        _BumpScale("BumpScale", float) = 1
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _Gloss("Gloss", Range(1, 40)) = 1 
    }
    
    SubShader{
        Pass{
                Tags{"LightMode" = "ForwardBase"}    
            
                CGPROGRAM
            
                #include "UnityCG.cginc"
                #include "Lighting.cginc"

                #pragma vertex  vert;
                #pragma fragment frag;

                float4 _Color;
                sampler2D _MainTex;
                float4 _MainTex_ST;
                sampler2D _BumpMap;
                float4 _BumpMap_ST;
                float _BumpScale;
                float4 _SpecularColor;
                float _Gloss;

                struct a2v
                {
                    float4 vert : POSITION;
                    float4 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float4 texCoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float4 uv : TEXCOORD0;
                    float4 TtoW0 : TEXCOORD1;
                    float4 TtoW1 : TEXCOORD2;
                    float4 TtoW2 : TEXCOORD3;
                };
    
                v2f vert(a2v i)
                {
                    v2f o;
                    //计算顶点本地左边转世界坐标
                    o.pos = UnityObjectToClipPos(i.vert);
                    //计算贴图和法线贴图的纹理左边
                    o.uv.xy = i.texCoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                    o.uv.zw = i.texCoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

                    float3 worldPos = UnityObjectToClipPos(i.vert);
                    float3 worldNormal = UnityObjectToWorldNormal(i.normal);
                    float3 worldTangent = UnityObjectToWorldDir(i.tangent);
                    float3 worldBiNormal = cross(worldNormal, worldTangent) * i.tangent.w;
                    o.TtoW0 = float4(worldBiNormal.x, worldTangent.x, worldNormal.x, worldPos.x);
                    o.TtoW1 = float4(worldBiNormal.y, worldTangent.y, worldNormal.y, worldPos.y);
                    o.TtoW2 = float4(worldBiNormal.z, worldTangent.z, worldNormal.z, worldPos.z);
                    
                    return o;
                }
    
                float4 frag(v2f i) : SV_Target
                {
                    float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
                    
                    //计算世界空间下的法线
                    float3 bump = UnpackNormal(tex2D(_BumpMap, i.uv.zw));
                    bump.xy = bump.xy * _BumpScale;
                    bump.z = sqrt(1 - saturate(dot(bump.xy, bump.xy)));
                    bump = normalize(float3(dot(i.TtoW0, bump), dot(i.TtoW1, bump), dot(i.TtoW2, bump)));
    
                    float3 worldLightDir = normalize(UnityWorldSpaceLightDir(worldPos));
                    float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
    
                    float3 albedo = tex2D(_MainTex, i.uv.xy).rgb * _Color; 
                    //计算环境光
                    float3 ambient = UNITY_LIGHTMODEL_AMBIENT * albedo;
                    //计算漫反射
                    float3 diffuse = _LightColor0.rgb * albedo * max(0, dot(bump, worldLightDir));
                    //计算高光
                    float3 halfDir = normalize(worldLightDir + worldViewDir);
                    float specular = _LightColor0.rgb * _SpecularColor * max(0, pow(dot(halfDir, bump), _Gloss));
                    
                    return float4(ambient + diffuse + specular, 1);
                }
                
                ENDCG
        }    
    }
}