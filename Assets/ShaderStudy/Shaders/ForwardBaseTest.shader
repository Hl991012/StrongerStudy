Shader"Study/SingleTexureMat"
{
//    Properties{
//        _Color("Color", Color) = (1, 1, 1, 1)
//        _Specular("Specular", Color) = (1, 1, 1, 1)
//        _Gloss("Gloss", Range(8, 256)) = 20
//    }
//
//    SubShader{
//        pass{
//            Tags{"LightMode" = "ForwardBase"}
//
//            CGPROGRAM
//
//            #pragma vertex vert
//            #pragma fragment frag
//
//            #pragma multi_compile_fwdbase
//
//            #include "UnityCG.cginc"
//            #include "Lighting.cginc"
//
//            float4 _Color;
//            float4 _Specular;
//            float _Gloss;
//
//            struct a2v
//            {
//                float4 vertex : POSITION;
//                float3 normal : NORMAL;
//            };
//
//            struct v2f
//            {
//                float4 vertex : SV_POSITION;
//                float3 worldNormal : TEXCOORD0;
//                float3 worldPos : TEXCOORD1;
//            };
//
//            v2f vert(a2v v)
//            {
//                v2f o;
//                o.vertex = UnityObjectToClipPos(v.vertex);
//                o.worldNormal = UnityObjectToWorldNormal(v.normal);
//                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
//                return o;
//            }
//
//            float4 frag(v2f i) : SV_Target    
//            {
//                float3 normal = normalize(i.worldNormal);
//                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
//                float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
//                //计算漫反射
//                float3 diffuse = _LightColor0.rgb * _Color.rgb * max(0, dot(normal, worldLightDir));
//                //计算高光
//                float3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
//                float3 halfDir = normalize(worldLightDir + viewDir);
//                float3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(normal, halfDir)), _Gloss);
//
//                //光照衰减
//                fixed atten = 1.0f;
//                
//                return float4(ambient + (diffuse + specular) * atten, 1);
//            }
//
//            ENDCG
//        }
//        
//        pass{
//        
//            Tags{"LightMode" = "ForwardAdd"} 
//            
//            Blend One One
//            
//            CGPROGRAM
//            
//            #pragma multi_compile_fwdadd
//            
//            #pragma vertex vert
//            #pragma fragment frag
//            
//            #include "Lighting.cginc"
//            #include "AutoLight.cginc"
//
//            float4 _Color;
//            float4 _Specular;
//            float _Gloss;
//
//            struct a2v
//            {
//                float4 vertex : POSITION;
//                float3 normal : NORMAL;
//            };
//
//            struct v2f
//            {
//                float4 vertex : SV_POSITION;
//                float3 worldNormal : TEXCOORD0;
//                float3 worldPos : TEXCOORD1;
//            };
//
//            v2f vert(a2v v)
//            {
//                v2f o;
//                o.vertex = UnityObjectToClipPos(v.vertex);
//                o.worldNormal = UnityObjectToWorldNormal(v.normal);
//                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
//                return o;
//            }
//
//            float4 frag(v2f i) : SV_Target
//            {
//                float3 normal = normalize(i.worldNormal);
//
//                //处理不同光照的方向
//                #ifdef USING_DIRECTIONAL_LIGHT
//                    float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
//                #else
//                    float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos) - i.worldPos.xyz);
//                #endif
//                
//                //计算漫反射
//                float3 diffuse = _LightColor0.rgb * _Color.rgb * max(0, dot(normal, worldLightDir));
//                //计算高光
//                float3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
//                float3 halfDir = normalize(worldLightDir + viewDir);
//                float3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(normal, halfDir)), _Gloss);
//
//                //处理不同光照的衰减因子
//                #if USING_DIRECTIONAL_lIGHT
//                    fixed atten = 1.0f;
//                #else
//                    float3 lightCoord = mul(unity_WorldToLight, float4(i.worldPos, 1)).xyz;
//                    fixed atten = tex2D(_LightTexture0, dot(lightCoord, lightCoord).rr).UNITY_ATTEN_CHANNEL;
//                #endif
//
//                
//                return float4((diffuse + specular) * atten, 1);
//            }
//
//            ENDCG
//        }
//    }
}